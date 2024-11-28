using System.Collections.ObjectModel;
using System.Reflection;

namespace SkyNET.Framework.Common.Dto {
    public static class DtoExtension {
        public static void CopyProperties<TObjFrom, TObjTo>(this TObjFrom objFrom, TObjTo objTo, ICollection<PropertyInfo> propsFrom = null, ICollection<PropertyInfo> propsTo = null)
            where TObjFrom : class
            where TObjTo : class, new() {
            if (objFrom == null)
                return;

            if (objTo == null)
                objTo = new TObjTo();

            if (propsFrom is null)
                propsFrom = typeof(TObjFrom).GetProperties();

            if (propsTo is null)
                propsTo = typeof(TObjTo).GetProperties();

            foreach (var propFrom in propsFrom) {
                var propTo = propsTo.FirstOrDefault(x => x.Name == propFrom.Name);
                if (propTo != null && propTo.PropertyType == propFrom.PropertyType) {
                    var v = propFrom.GetValue(objFrom);
                    propTo.SetValue(objTo, v);
                }
            }
        }

        public static void CopyProperties<TObjFrom, TObjTo>(this TObjFrom objFrom, out TObjTo objTo, ICollection<PropertyInfo> propsFrom = null, ICollection<PropertyInfo> propsTo = null)
            where TObjFrom : class
            where TObjTo : class, new() {
            objTo = new TObjTo();
            objFrom.CopyProperties(objTo);
        }

        public static void CopyProperties<TObjFrom, TObjTo>(this ICollection<TObjFrom> from, ICollection<TObjTo> to)
            where TObjFrom : class
            where TObjTo : class, new() {
            if (to == null)
                to = new List<TObjTo>();

            ICollection<PropertyInfo> propsFrom = null;
            ICollection<PropertyInfo> propsTo = null;

            foreach (var f in from) {
                f.CopyProperties(out TObjTo objTo, propsFrom, propsTo);
                to.Add(objTo);
            }
        }

        public static void CopyProperties<TObjFrom, TObjTo>(this ICollection<TObjFrom> from, out ICollection<TObjTo> to)
            where TObjFrom : class
            where TObjTo : class, new() {
            to = new List<TObjTo>();
            from.CopyProperties(to);
        }
        public static TObjTo CopyProperties<TObjFrom, TObjTo>(
            this TObjFrom from,
            Action<TObjFrom, TObjTo> func = null,
            ICollection<PropertyInfo> propsFrom = null,
            ICollection<PropertyInfo> propsTo = null
        )
            where TObjFrom : class
            where TObjTo : class, new() {
            if (from is null)
                return null;

            if (propsFrom is null)
                propsFrom = typeof(TObjFrom).GetProperties();

            if (propsTo is null)
                propsTo = typeof(TObjTo).GetProperties();

            var res = new TObjTo();

            foreach (var propFrom in propsFrom) {
                var propTo = propsTo.FirstOrDefault(x => x.Name == propFrom.Name);
                if (propTo != null && propTo.PropertyType == propFrom.PropertyType) {
                    var v = propFrom.GetValue(from);
                    propTo.SetValue(res, v);
                }
            }

            func?.Invoke(from, res);

            return res;
        }

        public static ICollection<TObjTo> CopyProperties<TObjFrom, TObjTo>(
            this ICollection<TObjFrom> from,
            Action<TObjFrom, TObjTo> func = null
        )
            where TObjFrom : class
            where TObjTo : class, new() {
            if (from is null)
                return null;

            ICollection<PropertyInfo> propsFrom = null;
            ICollection<PropertyInfo> propsTo = null;

            var res = new Collection<TObjTo>();
            foreach (var f in from) {
                var r = f.CopyProperties(func, propsFrom, propsTo);
                if (r != null)
                    res.Add(r);
            }

            return res;
        }

        public static TObjTo[] CopyPropertiesAsArray<TObjFrom, TObjTo>(
            this ICollection<TObjFrom> from,
            Action<TObjFrom, TObjTo> func = null
        )
            where TObjFrom : class
            where TObjTo : class, new() {
            var res = from.CopyProperties(func);
            return res?.ToArray();
        }
    }
}

