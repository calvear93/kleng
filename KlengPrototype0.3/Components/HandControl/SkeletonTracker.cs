using System;
using System.Windows.Media.Media3D;
using Kleng.Components.Exceptions;
using Microsoft.Kinect;

namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Skeleton tracker from Kinect's cameras.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.2.5</version>
    internal class SkeletonTracker : IDisposable
    {
        /// <summary>
        ///     Creates the skeleton tracker controller.
        /// </summary>
        /// <param name="kinect">Kinect sensor controller.</param>
        public SkeletonTracker(KinectController kinect)
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (kinect.Sensor == null)
                throw new KinectNotInitializedException(
                    "Kinect sensor not initialized or disconnected.\n\t[MISSING FUNCTION]: Connect.");

            // Initializes the skeletons array.
            SkeletonData = new Skeleton[kinect.Sensor.SkeletonStream.FrameSkeletonArrayLength];
            // Adds the functions to the events handlers.
            kinect.Sensor.SkeletonFrameReady += OnSkeletonFrameReady;
        }

        /// <summary>
        ///     Disposes the skeleton tracker controller.
        /// </summary>
        public void Dispose()
        {
            SkeletonData = null;
            SkeletonDataReadyEvent = null;
        }

        /// <summary>
        ///     Copies the skeleton information into an array. The function is
        ///     fired when any new frame is captured by sensor's cameras.
        /// </summary>
        /// <param name="sender">Object triggering to the function.</param>
        /// <param name="e">Event object with important information.</param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            // Gets the current skeleton frame captured.
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null && SkeletonData != null)
                {
                    // Copies skeleton data to an array of Skeletons, 
                    // where each Skeleton contains a collection of the joints.
                    frame.CopySkeletonDataTo(SkeletonData);

                    // Finds the tracked skeleton.
                    foreach (var skeleton in SkeletonData)
                    {
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            // Fires the Data Ready event and sends the tracked skeleton.
                            SkeletonDataReadyEvent?.Invoke(this, skeleton);
                        }
                    }
                }
            }
        }

        #region PROPERTIES

        /// <summary>
        ///     Array of Skeletons. Each Skeleton contains a collection of the joints.
        ///     Joints are outstanding zones of the skeleton tracked.
        /// </summary>
        public Skeleton[] SkeletonData { get; private set; }

        /// <summary>
        ///     Event handler for Skeleton Data Ready event.
        /// </summary>
        public event EventHandler<Skeleton> SkeletonDataReadyEvent;

        /// <summary>
        ///     Subscribes a function to Skeleton Data Ready event.
        ///     Is recommended to use OnSkeletonDataReady(object sender, Skeleton[] data)
        ///     header for listener function.
        /// </summary>
        public EventHandler<Skeleton> SkeletonDataReady
        {
            set { SkeletonDataReadyEvent += value; }
        }

        #endregion

        #region JOINT CALCS

        /// <summary>
        ///     Calculates the distance between two joint positions.
        /// </summary>
        /// <param name="j1">First joint.</param>
        /// <param name="j2">Second joint.</param>
        /// <returns>Distance between the two joints.</returns>
        public static double DistanceBetweenJoints(Joint j1, Joint j2)
        {
            return Math.Sqrt(
                Math.Pow(j1.Position.X - j2.Position.X, 2) +
                Math.Pow(j1.Position.Y - j2.Position.Y, 2) +
                Math.Pow(j1.Position.Z - j2.Position.Z, 2)
                );
        }

        /// <summary>
        ///     Calculates the angle between two joint positions.
        /// </summary>
        /// <param name="j1">First joint.</param>
        /// <param name="j2">Second joint.</param>
        /// <returns>Angle between two joints.</returns>
        public static double AngleBetweenJoints(Joint j1, Joint j2)
        {
            var v1 = new Vector3D(j1.Position.X, j1.Position.Y, j1.Position.Z);
            var v2 = new Vector3D(j2.Position.X, j2.Position.Y, j2.Position.Z);
            return Vector3D.AngleBetween(v1, v2);
        }

        /// <summary>
        ///     Calculates an intermediate position in order to do a smooth change from the current to the future one.
        /// </summary>
        /// <param name="x">Current horizontal position.</param>
        /// <param name="y">Current vertical position.</param>
        /// <param name="xf">Future horizontal position.</param>
        /// <param name="yf">Future vertical position.</param>
        /// <param name="roughness">Roughness (How resistant is the position change) percentage. From ]0,1].</param>
        /// <returns>Coordinates for new position.</returns>
        public static int[] PositionSmoothing(double x, double y, double xf, double yf, double roughness)
        {
            // Calculates the distance between axis coordinates and gets the distance smoothed.
            var xdiff = (xf - x)*roughness;
            var ydiff = (yf - y)*roughness;
            return new[] {(int) (x + xdiff), (int) (y + ydiff)};
        }

        #endregion
    }
}