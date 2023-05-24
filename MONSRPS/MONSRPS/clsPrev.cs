using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONSRPS
{
    class clsPrev
    {
        public static string group_Delete(object divisi, object departemen, object kode_divisimd)
        {
            return "DELETE DC_SRPS_DIV_T WHERE  DIVISI = TO_CHAR('" + divisi + "','FM00') AND DEPARTEMEN = TO_CHAR('" + departemen + "','FM00') AND KODE_DIVISIMD = TO_CHAR('" + kode_divisimd + "','FM000')";
        }

        public static string group_Update(string kode_dc, object divisi, object departemen, object kode_divisimd, string user)
        {
            return "UPDATE DC_SRPS_DIV_T SET KODE_DC = '" + kode_dc + "', KODE_DIVISIMD = '" + kode_divisimd + "', UPDREC_USER = '" + user + "'" +
                " WHERE DIVISI = TO_CHAR('" + divisi + "','FM00') AND DEPARTEMEN = TO_CHAR('" + departemen + "','FM00')";
        }

        public static string group_Insert(string kode_dc, object divisi, object departemen, object kode_divisimd, string user)
        {
            return "INSERT INTO DC_SRPS_DIV_T (KODE_DC, DIVISI, DEPARTEMEN, KODE_DIVISIMD, UPDREC_USER) " +
                "VALUES ('" + kode_dc + "', TO_CHAR('" + divisi + "','FM00'), TO_CHAR('" + departemen + "','FM00'), TO_CHAR('" + kode_divisimd + "','FM000'), '" + user + "')";
        }

        public static string group_Search(string kode)
        {
            return "SELECT DIVISI, DEPARTEMEN, KODE_DIVISIMD FROM DC_SRPS_DIV_T " +
                "WHERE DIVISI LIKE '%" + kode+ "%' OR DEPARTEMEN LIKE '%" + kode + "%' OR KODE_DIVISIMD LIKE '%" + kode + "%'";
        }

        //email

        public static string email_Delete(object kode_divisimd, object email)
        {
            return "DELETE DC_SRPS_EMAIL_T WHERE KODE_DIVISIMD = TO_CHAR('" + kode_divisimd + "','FM000') AND EMAIL = '" + email + "'";
        }

        public static string email_Update(string kode_dc, object kode_divisimd, object email, string user)
        {
            return "UPDATE DC_SRPS_EMAIL_T SET KODE_DC = '" + kode_dc + "', KODE_DIVISIMD = TO_CHAR('" + kode_divisimd + "','FM000'), EMAIL = '" + email + "', UPDREC_USER = '" + user + "' " +
                "WHERE KODE_DIVISIMD = TO_CHAR('" + kode_divisimd + "','FM000') AND EMAIL = '" + email + "'";
        }

        public static string email_UpdateSp(string kode_dc, object kode_divisimd, object email, string user)
        {
            return "UPDATE DC_SRPS_EMAIL_T SET KODE_DC = '" + kode_dc + "', KODE_DIVISIMD = '" + kode_divisimd + "', EMAIL = '" + email + "', UPDREC_USER = '" + user + "' " +
                "WHERE KODE_DIVISIMD = '" + kode_divisimd + "' AND EMAIL = '" + email + "'";
        }

        public static string email_Insert(string kode_dc, object kode_divisimd, object email, string user)
        {
            return "INSERT INTO DC_SRPS_EMAIL_T (KODE_DC, KODE_DIVISIMD, EMAIL, UPDREC_USER) " +
                "VALUES ('"+ kode_dc +"', TO_CHAR('" + kode_divisimd + "','FM000'), '" + email + "', '"+user+"')";
        }

        public static string email_InsertSp(string kode_dc, object kode_divisimd, object email, string user)
        {
            return "INSERT INTO DC_SRPS_EMAIL_T (KODE_DC, KODE_DIVISIMD, EMAIL, UPDREC_USER) " +
                "VALUES ('" + kode_dc + "', '" + kode_divisimd + "', '" + email + "', '" + user + "')";
        }

        public static string email_Search(string kode)
        {
            return "SELECT KODE_DIVISIMD, EMAIL FROM DC_SRPS_EMAIL_T " +
                "WHERE KODE_DIVISIMD LIKE '%"+kode+ "%' OR EMAIL LIKE '%" + kode + "%'";
        }

    }
}
