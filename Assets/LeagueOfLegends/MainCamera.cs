//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MainCamera.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.LeagueOfLegends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    /// <summary>
    /// The main camera
    /// </summary>
    public class MainCamera : MonoBehaviour
    {
        /// <summary>
        /// A list of items to try to include in the frame
        /// </summary>
        public List<GameObject> WatchItems;

        /// <summary>
        /// The multiplier that translates distance between objects to camera size
        /// </summary>
        public float DistanceToSizeScale;

        /// <summary>
        /// The camera compoentn
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// The default Y and Z position of the camera
        /// </summary>
        private float _defaultY;
        private float _defaultZ;

        /// <summary>
        /// Used for initialization
        /// </summary>
        protected void Start()
        {
            this._camera = this.GetComponent<Camera>();
            this._defaultY = this.transform.position.y;
            this._defaultZ = this.transform.position.z;
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        protected void Update()
        {
            var minX = this.WatchItems.Min(item => { return item.transform.position.x; });
            var maxX = this.WatchItems.Max(item => { return item.transform.position.x; });

            this.transform.position = new Vector3((minX + maxX) / 2, this._defaultY, this._defaultZ);
            this._camera.orthographicSize = Math.Max((maxX - minX) * this.DistanceToSizeScale, 3);
        }
    }
}
