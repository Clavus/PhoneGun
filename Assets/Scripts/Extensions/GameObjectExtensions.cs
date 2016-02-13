namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        /// <summary>
        ///     Check if this game object has a component of the given type.
        /// </summary>
        /// <param name="gameObject">GameObject</param>
        /// <returns>True when component is attached, otherwise false.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            var hasComponent = gameObject.GetComponent<T>() != null;
            return hasComponent;
        }
    }
}