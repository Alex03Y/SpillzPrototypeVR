namespace ProjectCore.Misc
{
    public static class UnityObjectUtils
    {
        public static bool IsNull(this UnityEngine.Object o)
        {
            return ReferenceEquals(o, null);
        }

        public static bool IsNotNull(this UnityEngine.Object o)
        {
            return !ReferenceEquals(o, null);
        }

        public static bool RefEquals(this UnityEngine.Object o, UnityEngine.Object obj)
        {
            return ReferenceEquals(o, obj);
        }

        public static bool InstanceIdEquals(this CachedBehaviour o, CachedBehaviour obj)
        {
            return o.InstanceId == obj.InstanceId;
        }
    }
}