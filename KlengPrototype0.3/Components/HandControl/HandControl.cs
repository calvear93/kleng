using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using Microsoft.Kinect;

namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Allows to controls the mouse cursor in
    ///     Windows by hand gestures.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.1.3</version>
    internal sealed class HandControl
    {
        /// <summary>
        ///     Stores a reference to kinect sensor controller.
        /// </summary>
        private readonly KinectController _kinect;

        /// <summary>
        ///     Creates the controller by hand gestures.
        /// </summary>
        /// <param name="kinect">Reference to the kinect controller.</param>
        /// <param name="skeletonTracker">Skeleton tracker of Kinect sensor.</param>
        /// <param name="primaryJoint">Most important joint of the user.</param>
        public HandControl(KinectController kinect, SkeletonTracker skeletonTracker, JointType primaryJoint)
        {
            // Stores the kinect controller reference.
            _kinect = kinect;
            // Subscribes the internal function to skeleton tracker event.
            skeletonTracker.SkeletonDataReady = OnSkeletonDataReady;
            // Creates the collection of gesture lock flags.
            _gestureLocks = new Dictionary<GestureLock, int>();
            // Initializes key value pair for gesture locks.
            foreach (GestureLock gesture in Enum.GetValues(typeof (GestureLock)))
                _gestureLocks.Add(gesture, 0);

            // Sets the joints.
            if ((PrimaryHand = primaryJoint) == JointType.HandRight)
            {
                PrimaryElbow = JointType.ElbowRight;
                PrimaryShoulder = JointType.ShoulderRight;
                SecondaryHand = JointType.HandLeft;
                SecondaryElbow = JointType.ElbowLeft;
                SecondaryShoulder = JointType.ShoulderLeft;
            }
            else
            {
                PrimaryElbow = JointType.ElbowLeft;
                PrimaryShoulder = JointType.ShoulderLeft;
                SecondaryHand = JointType.HandRight;
                SecondaryElbow = JointType.ElbowRight;
                SecondaryShoulder = JointType.ShoulderRight;
            }

            // Initializes the cache for the important joints.
            _primaryHandCache = new JointCache(KinectCapabilities.CacheSize);
            _secondaryHandCache = new JointCache(KinectCapabilities.CacheSize);
        }

        /// <summary>
        ///     Fired when a skeleton tracked is recognized by Skeleton Tracker.
        /// </summary>
        /// <param name="sender">Object triggering to the function.</param>
        /// <param name="data">Event object with important information.</param>
        private void OnSkeletonDataReady(object sender, Skeleton data)
        {
            // Adds the new joint tracked to the cache.
            _primaryHandCache.Add(data.Joints[PrimaryHand]);
            _secondaryHandCache.Add(data.Joints[SecondaryHand]);

            // Controls the cursor on the screen.
            CursorControl(data.Joints[PrimaryHand]);

            // Recognition of push gestures.
            if (VerifyJointPushGesture(_primaryHandCache))
            {
                MouseEmulator.LeftClick();
                // Clears the hand cache, 'cause one gesture was recognized already.
                _primaryHandCache.Clear();
                Thread.Sleep(KinectCapabilities.DepthTimeStabilization);
                return;
            }
            if (VerifyJointPushGesture(_secondaryHandCache))
            {
                MouseEmulator.RightClick();
                // Clears the hand cache, 'cause one gesture was recognized already.
                _secondaryHandCache.Clear();
                Thread.Sleep(KinectCapabilities.DepthTimeStabilization);
                return;
            }
            // Recognition of swipe gestures.
            if (VerifyJointSwipeUpGesture(_secondaryHandCache))
            {
                MouseEmulator.Wheel(KinectCapabilities.MouseWheelTriggerValue);
                // Clears the hand cache, 'cause one gesture was recognized already.
                _secondaryHandCache.Clear();
                // Locks the opposite gesture in the same axis, to avoid conflicts.
                LockGesture(GestureLock.SwipeDown, KinectCapabilities.GestureLockTime);
                return;
            }
            if (VerifyJointSwipeDownGesture(_secondaryHandCache))
            {
                MouseEmulator.Wheel(-KinectCapabilities.MouseWheelTriggerValue);
                // Clears the hand cache, 'cause one gesture was recognized already.
                _secondaryHandCache.Clear();
                // Locks the opposite gesture in the same axis, to avoid conflicts.
                LockGesture(GestureLock.SwipeUp, KinectCapabilities.GestureLockTime);
                return;
            }
            // Secondary hand touches primary elbow.
            if (SkeletonTracker.DistanceBetweenJoints(data.Joints[SecondaryHand], data.Joints[PrimaryElbow]) <
                KinectCapabilities.NearDistance)
                MouseEmulator.DoubleLeftClick();
        }

        /// <summary>
        ///     Sets the cursor position for joint control.
        /// </summary>
        /// <param name="primaryHand">Primary joint for cursor control.</param>
        private void CursorControl(Joint primaryHand)
        {
            // Stabilizes the cursor movement in depth gestures.
            if (VerifyDepthStabilization(_primaryHandCache))
                return;
            // Map out the joint position to the screen.
            var mappedPoint = _kinect.MapSkeletonPoint(primaryHand, SystemParameters.PrimaryScreenWidth,
                SystemParameters.PrimaryScreenHeight);
            // Gets the cursor current position.
            var cursorPosition = MouseEmulator.GetCursorPosition();
            // Makes the new cursor position smoothness.
            var fixedPosition = SkeletonTracker.PositionSmoothing(cursorPosition[0], cursorPosition[1], mappedPoint.X,
                mappedPoint.Y, KinectCapabilities.RoughnessFactor);
            // Sets the new cursor position.
            MouseEmulator.SetCursorPosition(fixedPosition[0], fixedPosition[1]);
        }

        #region CACHE PROPERTIES

        /// <summary>
        ///     Primary hand for skeleton recognition.
        /// </summary>
        public JointType PrimaryHand { get; }

        /// <summary>
        ///     Secondary hand for skeleton recognition.
        /// </summary>
        public JointType SecondaryHand { get; }

        /// <summary>
        ///     Primary elbow for skeleton recognition.
        /// </summary>
        public JointType PrimaryElbow { get; }

        /// <summary>
        ///     Secondary elbow for skeleton recognition.
        /// </summary>
        public JointType SecondaryElbow { get; }

        /// <summary>
        ///     Primary shoulder for skeleton recognition.
        /// </summary>
        public JointType PrimaryShoulder { get; }

        /// <summary>
        ///     Secondary shoulder for skeleton recognition.
        /// </summary>
        public JointType SecondaryShoulder { get; }

        /// <summary>
        ///     Primary hand cache to store the last joints.
        /// </summary>
        private readonly JointCache _primaryHandCache;

        /// <summary>
        ///     Secondary hand cache to store the last joints.
        /// </summary>
        private readonly JointCache _secondaryHandCache;

        #endregion

        #region GESTURE LOCKS

        /// <summary>
        ///     Enum with gesture constants.
        /// </summary>
        private enum GestureLock
        {
            Pull,
            Push,
            SwipeLeft,
            SwipeRight,
            SwipeDown,
            SwipeUp,
            Closeness
        }

        /// <summary>
        ///     Collection of gesture lock flags and counters.
        /// </summary>
        private readonly Dictionary<GestureLock, int> _gestureLocks;

        #endregion

        #region GESTURES RECOGNITION

        /// <summary>
        ///     Verifies if a push gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a push gesture is recognized, otherwise false.</returns>
        private bool VerifyJointPushGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.Push] > 0)
                return false;

            return (cache.Last().Position.Z - cache.ZAverage) < -KinectCapabilities.DepthSensitivity;
        }

        /// <summary>
        ///     Verifies if a pull gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a pull gesture is recognized, otherwise false.</returns>
        private bool VerifyJointPullGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.Pull] > 0)
                return false;

            return (cache.ZAverage - cache.Last().Position.Z) > KinectCapabilities.DepthSensitivity;
        }

        /// <summary>
        ///     Verifies if a swipe left gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a swipe left gesture is recognized, otherwise false.</returns>
        private bool VerifyJointSwipeLeftGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.SwipeLeft] > 0)
                return false;

            return (cache.XAverage - cache.Last().Position.X) > KinectCapabilities.HorizontalSensitivity;
        }

        /// <summary>
        ///     Verifies if a swipe right gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a swipe right gesture is recognized, otherwise false.</returns>
        private bool VerifyJointSwipeRightGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.SwipeRight] > 0)
                return false;

            return (cache.XAverage - cache.Last().Position.X) < -KinectCapabilities.HorizontalSensitivity;
        }

        /// <summary>
        ///     Verifies if a swipe down gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a swipe down gesture is recognized, otherwise false.</returns>
        private bool VerifyJointSwipeDownGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.SwipeDown] > 0)
                return false;

            return (cache.YAverage - cache.Last().Position.Y) > KinectCapabilities.VerticalSensitivity;
        }

        /// <summary>
        ///     Verifies if a swipe up gesture was did.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a swipe up gesture is recognized, otherwise false.</returns>
        private bool VerifyJointSwipeUpGesture(JointCache cache)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.SwipeUp] > 0)
                return false;

            return (cache.YAverage - cache.Last().Position.Y) < -KinectCapabilities.VerticalSensitivity;
        }

        /// <summary>
        ///     Verifies the closeness between two joint caches.
        /// </summary>
        /// <param name="j1">First joint cache.</param>
        /// <param name="j2">SEcond joint cache.</param>
        /// <returns>True if joints are close, otherwise false.</returns>
        private bool VerifyCloseness(JointCache j1, JointCache j2)
        {
            // Verifies the lock state.
            if (_gestureLocks[GestureLock.Closeness] > 0)
                return false;

            return j1.DistanceAverage(j2) < KinectCapabilities.NearDistance;
        }

        /// <summary>
        ///     Verifies if is necessary to stabilize the depth movement.
        /// </summary>
        /// <param name="cache">Cache related to the joint verified.</param>
        /// <returns>True if a stabilization is required, otherwise false.</returns>
        private static bool VerifyDepthStabilization(JointCache cache)
        {
            var depthdiff = cache.Last().Position.Z - cache.First().Position.Z;
            return depthdiff < -KinectCapabilities.PushStabilization || depthdiff > KinectCapabilities.PullStabilization;
        }

        /// <summary>
        ///     Allows to lock a gesture by some time.
        /// </summary>
        /// <param name="gestureLock">Gesture lock flag passing by reference.</param>
        /// <param name="miliseconds">Lock time remaining.</param>
        private void LockGesture(GestureLock gestureLock, int miliseconds)
        {
            // Verifies if is necessary to increase lock time.
            if (miliseconds < _gestureLocks[gestureLock])
                return;
            // Creates a timer for unlock the gesture.
            if (_gestureLocks[gestureLock] == 0)
            {
                var timer = new TimedThreadHandler(1);
                timer.IntervalElapsed = (o, sender) =>
                {
                    _gestureLocks[gestureLock]--;
                    if (_gestureLocks[gestureLock] == 0)
                        timer.Stop();
                };
                timer.Start();
            }
            // Sets the new time lock.
            _gestureLocks[gestureLock] = miliseconds;
        }

        #endregion
    }
}