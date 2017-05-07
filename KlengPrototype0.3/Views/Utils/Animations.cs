using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kleng.Views.Utils
{
    /// <summary>
    ///     Animations library for UI elements.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.1</version>
    internal static class Animations
    {
        /// <summary>
        ///     Blinking animation for a UI element.
        /// </summary>
        /// <param name="element">UI element to will be animated.</param>
        /// <param name="duration">Animation cycle duration.</param>
        /// <param name="from">Starting transparency value for each animation cycle.</param>
        /// <param name="to">Ending transparency value for each animation cycle.</param>
        /// <param name="autoreverse">Boolean indicating if the element will be returns to original state.</param>
        /// <param name="forever">Boolean indicating if the animation will be iterated forever.</param>
        /// <param name="ease">Boolean indicating if the animation will be softened.</param>
        public static void Blink(UIElement element, double duration = 0.5, double from = 1,
            double to = 0, bool autoreverse = true, bool forever = false, bool ease = false)
        {
            // Animation object.
            var animation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(duration)))
            {
                AutoReverse = autoreverse
            };
            if (forever)
                animation.RepeatBehavior = RepeatBehavior.Forever; // Repeats forever.
            if (ease)
                animation.EasingFunction = new PowerEase {EasingMode = EasingMode.EaseInOut}; // Softened animation.
            // Starts the animation.
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        /// <summary>
        ///     Rotation animation for a UI element.
        /// </summary>
        /// <param name="element">UI element to will be animated.</param>
        /// <param name="duration">Animation cycle duration.</param>
        /// <param name="from">Starting angle value for each animation cycle.</param>
        /// <param name="to">Ending angle value for each animation cycle.</param>
        /// <param name="autoreverse">Boolean indicating if the element will be returns to original state.</param>
        /// <param name="forever">Boolean indicating if the animation will be iterated forever.</param>
        /// <param name="ease">Boolean indicating if the animation will be softened.</param>
        /// <param name="x">Origin horizontal coordinate for the animation.</param>
        /// <param name="y">Origin vertical coordinate for the animation.</param>
        public static void Rotate(UIElement element, double duration = 0.5, double from = 0, double to = 360,
            bool autoreverse = true, bool forever = false, bool ease = false, double x = 0.5, double y = 0.5)
        {
            // Animation object.
            var animation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(duration)))
            {
                AutoReverse = autoreverse
            };
            if (forever)
                animation.RepeatBehavior = RepeatBehavior.Forever; // Repeats forever.
            if (ease)
                animation.EasingFunction = new PowerEase {EasingMode = EasingMode.EaseInOut}; // Softened animation.
            // Rotation.
            var rotate = new RotateTransform();
            element.RenderTransform = rotate;
            // Origin point where rotation will be performed.
            element.RenderTransformOrigin = new Point(x, y);
            // Starts the animation.
            rotate.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        /// <summary>
        /// </summary>
        /// <param name="element">UI element to will be animated.</param>
        /// <param name="duration">Animation cycle duration.</param>
        /// <param name="from">Starting angle value for each animation cycle.</param>
        /// <param name="to">Ending angle value for each animation cycle.</param>
        /// <param name="autoreverse">Boolean indicating if the element will be returns to original state.</param>
        /// <param name="forever">Boolean indicating if the animation will be iterated forever.</param>
        /// <param name="ease">Boolean indicating if the animation will be softened.</param>
        /// <param name="x">Origin horizontal coordinate for the animation.</param>
        /// <param name="y">Origin vertical coordinate for the animation.</param>
        public static void Zoom(UIElement element, double duration = 1, double from = 1, double to = 1.5,
            bool autoreverse = true, bool forever = false, bool ease = false, double x = 0.5, double y = 0.5)
        {
            // Animation object.
            var animation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(duration)))
            {
                AutoReverse = autoreverse
            };
            if (forever)
                animation.RepeatBehavior = RepeatBehavior.Forever; // Repeats forever.
            if (ease)
                animation.EasingFunction = new PowerEase {EasingMode = EasingMode.EaseInOut}; // Softened animation.
            // Zoom.
            var zoom = new ScaleTransform();
            element.RenderTransform = zoom;
            // Origin point where rotation will be performed.
            element.RenderTransformOrigin = new Point(x, y);
            // Starts the expansion animation for each coordinate.
            zoom.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            zoom.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        ///     Highlights the UI element.
        /// </summary>
        /// <param name="element">UI element to will be highlighted.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static void Highlight(UIElement element, float duration = 0.5f)
        {
            Blink(element, duration, element.Opacity);
        }

        /// <summary>
        ///     Blinking indefinitely the UI element.
        /// </summary>
        /// <param name="element">UI element that will blink.</param>
        /// <param name="duration">Duration of the each animation cycle.</param>
        public static void StartBlinking(UIElement element, float duration = 0.5f)
        {
            Blink(element, duration, element.Opacity, 0, true, true);
        }

        /// <summary>
        ///     Blinking soft and indefinitely the UI element.
        /// </summary>
        /// <param name="element">UI element that will blink softly.</param>
        /// <param name="duration">Duration of the each animation cycle.</param>
        public static void StartSoftBlinking(UIElement element, float duration = 0.5f)
        {
            Blink(element, duration, element.Opacity, 0, true, true, true);
        }

        /// <summary>
        ///     Stops the current UI element blinking animation.
        /// </summary>
        /// <param name="element">UI element.</param>
        public static void StopBlinking(UIElement element)
        {
            element.BeginAnimation(UIElement.OpacityProperty, null);
        }

        /// <summary>
        ///     Rotates indefinitely the UI element.
        /// </summary>
        /// <param name="element">UI element that will rotated.</param>
        /// <param name="duration">Duration of the each animation cycle.</param>
        public static void StartRotating(UIElement element, float duration)
        {
            Rotate(element, duration, 0, 360, false, true);
        }

        /// <summary>
        ///     Rotates soft and indefinitely the UI element.
        /// </summary>
        /// <param name="element">UI element that will rotated softly.</param>
        /// <param name="duration">Duration of the each animation cycle.</param>
        public static void StartSoftRotating(UIElement element, float duration)
        {
            Rotate(element, duration, 0, 360, false, true, true);
        }

        /// <summary>
        ///     Rotates the UI element once.
        /// </summary>
        /// <param name="element">UI element that will rotated.</param>
        /// <param name="duration">Duration of the each animation.</param>
        /// <param name="from">Starting angle value.</param>
        /// <param name="to">Ending angle value.</param>
        public static void Rotate(UIElement element, float duration, int from, int to)
        {
            Rotate(element, duration, from, to, false);
        }

        /// <summary>
        ///     Rotates softly the UI element once.
        /// </summary>
        /// <param name="element">UI element that will rotated softly.</param>
        /// <param name="duration">Duration of the each animation.</param>
        /// <param name="from">Starting angle value.</param>
        /// <param name="to">Ending angle value.</param>
        public static void SoftRotate(UIElement element, float duration, int from, int to)
        {
            Rotate(element, duration, from, to, false, false, true);
        }

        /// <summary>
        ///     Stops the current UI element rotation animation.
        /// </summary>
        /// <param name="element">UI element.</param>
        public static void StopRotating(UIElement element)
        {
            element.BeginAnimation(RotateTransform.AngleProperty, null);
        }
    }
}