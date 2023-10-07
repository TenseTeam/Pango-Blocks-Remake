namespace VUDK.Extensions.GameObject
{
    using UnityEngine;

    public static class GameObjectExtension
    {
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            if(gameObject == null)
                return false;

            return layerMask == (layerMask | (1 << gameObject.layer));
        }
    }
}