using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Kleng.Components;
using Kleng.Views.Utils;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for SaveReportView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0.7</version>
    public partial class SaveReportView : Window
    {
        private readonly string _audioName;
        private readonly string _audioPath;
        private readonly Window _parent;
        private readonly string _path;
        private readonly Result _results;

        public SaveReportView(Window parent, Result results, string path, string audioPath = null,
            string audioSaved = null)
        {
            InitializeComponent();
            _parent = parent;
            _results = results;
            _audioPath = audioPath;
            _audioName = audioSaved;
            _path = path;
            FileUtils.CreateDirectory(_path);
            if (_audioPath != null)
                FileUtils.CreateDirectory(_audioPath);
            title.Text = "Título: " + _results.Title;
            corrects.Text = "Correctas: " + _results.Corrects;
            time.Text = "Tiempo demorado: " + _results.TimeConsuming;
            wrongs.Text = "Incorrectas: " + _results.Wrongs;
            if (_results.Score != null)
                modulation.Text = "Modulación: " + _results.Score;
            else
                modulation.Text = "";
            Cursor = CursorsUtils.Arrow();
            Back_Button.Cursor = CursorsUtils.Link();
            SaveReport.Cursor = CursorsUtils.Link();
            Name.Cursor = CursorsUtils.Text();
            RUN.Cursor = CursorsUtils.Text();
            Teacher.Cursor = CursorsUtils.Text();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateRun(RUN.Text))
            {
                MessageUtils.ShowError("Problema con el formulario.", "R.U.N. incorrecto.");
                return;
            }
            if (Name.Text == "" || Teacher.Text == "")
            {
                MessageUtils.ShowWarn("Problema con el formulario.", "Complete todos los campos por favor.");
                return;
            }

            var currentTime = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            FileUtils.CreateDirectory(_path + "/" + currentTime);

            var doc = new Document(new Rectangle(384, 512));
            doc.SetMargins(25, 25, 30, 30);
            PdfWriter.GetInstance(doc,
                new FileStream(_path + "/" + currentTime + "/report" + currentTime + ".pdf", FileMode.Create));

            doc.AddTitle("Report Kleng " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            doc.AddCreator("Kleng 0.3.1");

            doc.Open();

            var stamp = Image.GetInstance("Images/stamp_company.png");
            stamp.ScaleToFit(100f, 90f);
            stamp.SpacingBefore = 10f;
            stamp.SpacingAfter = 1f;
            stamp.Alignment = Image.TEXTWRAP | Element.ALIGN_LEFT;

            doc.Add(stamp);

            doc.Add(new Paragraph(_results.Date) {Alignment = Element.ALIGN_RIGHT});
            doc.Add(new Paragraph(_results.Time) {Alignment = Element.ALIGN_RIGHT});

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);

            doc.Add(new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1)));
            doc.Add(CreateParagraph("<b>Nombre:</b> " + Name.Text + "."));
            doc.Add(
                CreateParagraph("<b>R.U.N.:</b> " +
                                Regex.Replace(RUN.Text,
                                    @"^([0-9]{2})[.]{0,1}([0-9]{3})[.]{0,1}([0-9]{3})-{0,1}([0-9k])$", @"$1.$2.$3-$4")));
            doc.Add(CreateParagraph("<b>Nombre Profesor:</b> " + Teacher.Text + "."));
            doc.Add(new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1)));

            doc.Add(Chunk.NEWLINE);

            doc.Add(CreateParagraph("<b>Título:</b> " + _results.Title));
            doc.Add(Chunk.NEWLINE);
            doc.Add(CreateParagraph("<b>Tiempo:</b> " + _results.TimeConsuming));
            doc.Add(CreateParagraph("<b>Palabras correctas:</b> " + _results.Corrects));
            doc.Add(CreateParagraph("<b>Palabras incorrectas:</b> " + _results.Wrongs));
            if (_results.Score != null)
                doc.Add(CreateParagraph("<b>Modulación:</b> " + _results.Score + "."));
            doc.Add(new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1)));

            var kleng = Image.GetInstance("Images/logo_kleng.png");
            kleng.ScaleToFit(70f, 60f);
            kleng.SpacingBefore = 16f;
            kleng.Alignment = Element.ALIGN_BOTTOM;

            doc.Add(kleng);

            doc.Close();

            if (_audioName != null)
                FileUtils.MoveFile(_audioPath + "/" + _audioName, _path + "/" + currentTime + "/" + _audioName);

            MessageUtils.ShowSuccess("Generación de reporte.",
                "Reporte generado satisfactoriamente en: " + _path + "/" + currentTime + "/");

            _parent.Show();
            Close();
        }

        private static Paragraph CreateParagraph(string text)
        {
            var p = new Paragraph(1);
            p.SetLeading(1, 0.8f);

            using (var sr = new StringReader(text))
            {
                var elements = HTMLWorker.ParseToList(sr, null);
                foreach (var e in elements)
                {
                    p.Add(e);
                }
            }
            return p;
        }

        private static bool ValidateRun(string run)
        {
            return
                new Regex("^([0-9]{2})[.]{0,1}([0-9]{3})[.]{0,1}([0-9]{3})-{0,1}([0-9k])$", RegexOptions.IgnoreCase)
                    .Match(run).Success;
        }
    }
}