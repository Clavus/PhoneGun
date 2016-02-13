namespace UnityEngine
{
    public static class ComponentExtensions
    {
        /// <summary>
        ///     Check if this component's game object has a component of the given type.
        /// </summary>
        /// <param name="component">Component</param>
        /// <returns>True when component is attached, otherwise false.</returns>
        public static bool HasComponent<T>(this Component component) where T : Component
        {
            var hasComponent = component.GetComponent<T>() != null;
            return hasComponent;
        }
    }
}