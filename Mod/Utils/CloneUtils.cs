using System;
using System.Reflection;
using UnityEngine;

namespace OutwardArchipelago.Utils
{
    internal static class CloneUtils
    {
        /// <summary>
        /// Create a new default instance of the specified type.
        /// If it is a Component, it will be added to a new GameObject and returned.
        /// Otherwise, a new instance will be created using the default constructor.
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <returns>A new default instance.</returns>
        public static object CreateInstance(Type type)
        {
            if (type.IsSubclassOf(typeof(Component)))
            {
                var name = UID.Generate().ToString();
                var obj = new GameObject(name);
                obj.SetActive(false);
                return obj.AddComponent(type);
            }

            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Create a new default instance of the specified type.
        /// If it is a Component, it will be added to a new GameObject and returned.
        /// Otherwise, a new instance will be created using the default constructor.
        /// </summary>
        /// <typeparam name="T">The type to create.</typeparam>
        /// <returns>A new default instance.</returns>
        public static T CreateInstance<T>() where T : class
        {
            return CreateInstance(typeof(T)) as T;
        }

        /// <summary>
        /// Deep copies all serializable members from <see cref="source"/> to <see cref="dest"/>.
        /// </summary>
        /// <param name="source">The object to copy from.</param>
        /// <param name="dest">The object to copy to.</param>
        public static void DeepCopy(object source, object dest)
        {
            var type = source.GetType();
            while (type is not null)
            {
                if (type.IsInstanceOfType(dest))
                {
                    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (field.IsPublic || field.GetCustomAttribute<SerializeField>() is not null)
                        {
                            field.SetValue(dest, DeepClone(field.GetValue(source)));
                        }
                    }
                }

                type = type.BaseType;
            }
        }

        /// <summary>
        /// Creates a deep clone of a Unity serializable object.
        /// </summary>
        /// <param name="type">The type to treat the source as.</param>
        /// <param name="source">The object to copy.</param>
        /// <returns>A deep clone of the object.</returns>
        public static object DeepClone(object source)
        {
            if (source is null)
            {
                return null;
            }

            var type = source.GetType();
            if (type.IsValueType || type == typeof(string) || type == typeof(Sprite) || type == typeof(UID))
            {
                return source;
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var array = source as Array;
                var destArray = Array.CreateInstance(elementType, array.Length);
                for (var i = 0; i < array.Length; i++)
                {
                    destArray.SetValue(DeepClone(array.GetValue(i)), i);
                }
                return destArray;
            }

            if (source is ICloneable clonable)
            {
                return clonable.Clone();
            }

            var dest = CreateInstance(type);
            if (source is Component sourceComponent && dest is Component destComponent)
            {
                destComponent.gameObject.name = $"{sourceComponent.gameObject.name} (Clone)";
            }

            DeepCopy(source, dest);

            return dest;
        }

        public static T DeepClone<T>(T source) where T : class
        {
            return DeepClone((object)source) as T;
        }
    }
}
