using System.Collections.Generic;
using ProjectCore.Misc;
using UnityEngine;

namespace ProjectCore.Utilities
{
    public class BoundingBoxPositionClamp : CachedBehaviour
    {
        public enum UpdateType
        {
            Update, LateUpdate, FixedUpdate
        }
        
        private class TrackingObject
        {
            public Vector3 PreviouslyPosition;
            public readonly Transform Transform;

            public TrackingObject(Transform transform, Vector3 previouslyPosition)
            {
                Transform = transform;
                PreviouslyPosition = previouslyPosition;
            }
        }
        
        public Transform[] TrackingObjects;
        public Vector3 BoundsScale;
        public UpdateType PositionUpdateType = UpdateType.LateUpdate;
        
        private Bounds _bounds;
        private List<TrackingObject> _trackingObjects = new List<TrackingObject>();
        
        private void Awake()
        {
            _bounds = new Bounds(Transform.Value.position, BoundsScale);

            foreach (var trackingObject in TrackingObjects)
                _trackingObjects.Add(new TrackingObject(trackingObject, trackingObject.position));
        }

        private void Update()
        {
            if(PositionUpdateType == UpdateType.Update)
                ProcessingPosition();
        }

        private void LateUpdate()
        {
            if(PositionUpdateType == UpdateType.LateUpdate)
                ProcessingPosition();
        }

        private void FixedUpdate()
        {
            if(PositionUpdateType == UpdateType.FixedUpdate)
                ProcessingPosition();
        }

        private void ProcessingPosition()
        {
            foreach (var trackingObject in _trackingObjects)
            {
                var position = trackingObject.Transform.position;

                if (IsInsideBounds(position))
                    trackingObject.PreviouslyPosition = position;
                else
                {
                    trackingObject.PreviouslyPosition = _bounds.ClosestPoint(trackingObject.Transform.position);
                    trackingObject.Transform.position = trackingObject.PreviouslyPosition;
                }
            }
        }

        private bool IsInsideBounds(Vector3 position)
        {
            return _bounds.Contains(position);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var cubePosition = Transform.Value.position;
            
            Gizmos.color = new Color(0, 0, 0, 1);
            Gizmos.DrawWireCube(cubePosition, BoundsScale);
            
            Gizmos.color = new Color(0.5f, 1, 0.5f, 0.2f);
            Gizmos.DrawCube(cubePosition, BoundsScale);
        }
#endif
    }
}
