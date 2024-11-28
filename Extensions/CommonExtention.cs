using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SkyNET.Framework.Common.Extensions {
    public static class CommonExtention {
        /// <summary>
        /// Является ли значение значением по умолчанию
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyHasDefaultValue(this object obj, string property) {
            if (obj == null || string.IsNullOrEmpty(property))
                return false;

            // проверим наличие нужного свойства
            PropertyInfo pi = obj.GetType().GetProperty(property);
            if (pi == null)
                return false;

            // получим экземпляр DefaultValueAttribute для объектов этого типа
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(obj)[property].Attributes;
            DefaultValueAttribute myAttribute =
               (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];

            if (myAttribute == null)
                return false;

            // сравним значения
            object v1 = myAttribute.Value;
            object v2 = pi.GetValue(obj, null);
            if (v1.ToString() != v2.ToString())
                return false;

            return true;
        }

        /// <summary>
        /// Выполняет "глубокое" копирование объекта. Последний должен быть сериализуем!
        /// </summary>
        /// <param name="value">Оригинал</param>
        /// <returns>Копия объекта</returns>
        //public static T DeepCopy<T>(T value) where T : class {
        //    var f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        //    using (var stream = new MemoryStream()) {
        //        f.Serialize(stream, value);
        //        stream.Position = 0;
        //        return (T)f.Deserialize(stream);
        //    }
        //}

        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN<T>(this T val, params T[] values) {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i].Equals(val))
                    return true;
            return false;
        }

        /// <summary>
        /// Возвращает строку из названий свойства и значения свойства
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetMsgFromAllProps<T>(this T obj) where T : class {
            try {
                if (obj == null) {
                    return string.Empty;
                }
                var props = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                int cnt = props.Length;
                if (cnt == 0) {
                    return string.Empty;
                } else if (cnt == 1) {
                    return $"{props[0].Name}={props[0].GetValue(obj)}";
                } else {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < cnt; i++) {
                        sb.Append($"{props[i].Name}={props[i].GetValue(obj)}{i}");
                        if (i < cnt - 1) {
                            sb.Append(", ");
                        };
                    }
                    return sb.ToString();
                }
            } catch {
                return $"Ошибка при формировании строки свойств-значений для объекта {nameof(T)}";
            }
        }
    }
}
