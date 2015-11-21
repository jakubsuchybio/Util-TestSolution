using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace InsensitiveInvariantForCzech
{
    public class InsensitiveInvariant : IComparer<string>, IEqualityComparer<string>
    {
        public static readonly InsensitiveInvariant Instance = new InsensitiveInvariant();

        private InsensitiveInvariant() {
        }

        public static CompareInfo CompareInfo { get; } = CultureInfo.InvariantCulture.CompareInfo;

        public static IComparer<string> Comparer => Instance;

        public static IEqualityComparer<string> EqualityComparer => Instance;

        #region IComparer

        public int Compare(string x, string y) {
            if( x == null && y == null )
                return 0;
            if( x == null )
                return 1;
            if( y == null )
                return -1;

            return CompareInfo.Compare( x, y, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace );
        }

        #endregion IComparer

        #region IEqualityComparer

        public bool Equals(string x, string y) {
            return Instance.Compare( x, y ) == 0;
        }

        public int GetHashCode(string obj) {
            return obj != null ? RemoveDiacritics( obj ).ToUpperInvariant().GetHashCode() : 0;
        }

        #endregion IEqualityComparer

        public static int CompareValues(string x, string y) {
            return Instance.Compare( x, y );
        }

        public static bool Contains(string x, string y) {
            if( x == null || y == null || x.Length < y.Length )
                return false;

            return CompareInfo.IndexOf( x, y, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace ) >= 0;
        }

        public static bool EndsWith(string x, string y) {
            if( x == null || y == null || x.Length < y.Length )
                return false;

            return CompareInfo.IsSuffix( x, y, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace );
        }

        public static bool EqualsValues(string x, string y) {
            return Instance.Compare( x, y ) == 0;
        }

        public static string RemoveDiacritics(string text) {
            if( text == null )
                return null;

            return string.Concat(
                text.Normalize( NormalizationForm.FormD )
                .Where( ch => CharUnicodeInfo.GetUnicodeCategory( ch ) !=
                                                 UnicodeCategory.NonSpacingMark )
            ).Normalize( NormalizationForm.FormC );
        }

        public static bool StartsWith(string x, string y) {
            if( x == null || y == null || x.Length < y.Length )
                return false;

            return CompareInfo.IsPrefix( x, y, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace );
        }
    }
}