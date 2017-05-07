using System.Collections.Generic;
using System.Linq;
using Microsoft.Kinect;

namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Collection inherited from List. Permits
    ///     to store Joint object as a piece of limited memory.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.2.9</version>
    internal class JointCache : List<Joint>
    {
        /// <summary>
        ///     Creates the list that simulates a cache structure.
        /// </summary>
        /// <param name="cacheSize">Size of the cache.</param>
        public JointCache(int cacheSize)
        {
            CacheLimit = cacheSize;
        }

        #region PROPERTIES

        /// <summary>
        ///     Limit of Joint stored in the cache.
        /// </summary>
        public int CacheLimit;

        /// <summary>
        ///     Calculates the X coordinate average.
        /// </summary>
        public double XAverage => this.Average(var => var.Position.X);

        /// <summary>
        ///     Calculates the Y coordinate average.
        /// </summary>
        public double YAverage => this.Average(var => var.Position.Y);

        /// <summary>
        ///     Calculates the Z coordinate average.
        /// </summary>
        public double ZAverage => this.Average(var => var.Position.Z);

        #endregion

        #region FUNCTIONS

        /// <summary>
        ///     Adds the Joint object.
        /// </summary>
        /// <param name="joint">Joint to be will stored.</param>
        public new virtual void Add(Joint joint)
        {
            // If the stored joint exceeds the limit,
            // the oldest joint is removed.
            if (Count > CacheLimit)
                RemoveAt(0);

            base.Add(joint);
        }

        /// <summary>
        ///     Calculates the distance average between two joints cache.
        /// </summary>
        /// <param name="external">External cache.</param>
        /// <returns>Distance average between two joints cache.</returns>
        public double DistanceAverage(JointCache external)
        {
            // Stores the temporal sum of distances.
            // Unify the lists in one structure.
            var joints = this.Zip(external, (p, s) => new {Primary = p, Secondary = s});
            var sum = joints.Sum(cache => SkeletonTracker.DistanceBetweenJoints(cache.Primary, cache.Secondary));
            // Divides the sum and returns the final average.
            return sum/KinectCapabilities.CacheSize;
        }

        #endregion
    }
}