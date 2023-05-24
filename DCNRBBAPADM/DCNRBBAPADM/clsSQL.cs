using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCNRBBAPADM
{
    class clsSQL
    {
        public static string PenundaanBAP_Administratif()
        {
            return "SELECT " +
                   "a.KODE_DC, a.UPDREC_DATE, a.PLUID, b.TAG, b.TGL_BAPSEPIHAK, " +
                   "CASE WHEN b.TAG IN('N','F','R') THEN '36' END AS JML_HARI," +
                   "CASE WHEN b.TAG IN('N','F','R') THEN b.TGL_BAPSEPIHAK + 36 END AS USULAN_BARU_BAP " +
                   "FROM DC_TUNDA_BAPADM_T a JOIN DC_SRPS_T  b ON (a.pluid = b.pluid) ";
        }

        public static string FindPenundaanBAP_Administratif(string find)
        {
            return "SELECT " +
                   "a.KODE_DC, a.UPDREC_DATE, a.PLUID, b.TAG, b.TGL_BAPSEPIHAK, " +
                   "CASE WHEN b.TAG IN('N','F','R') THEN '36' END AS JML_HARI," +
                   "CASE WHEN b.TAG IN('N','F','R') THEN b.TGL_BAPSEPIHAK + 36 END AS USULAN_BARU_BAP " +
                   "FROM DC_TUNDA_BAPADM_T a JOIN DC_SRPS_T  b ON (a.pluid = b.pluid) " +
                   "WHERE a.KODE_DC LIKE  '%" + find + "%' OR WHERE a.PLUID LIKE  '%" + find + "%' OR WHERE b.TGL_BAPSEPIHAK LIKE  '%" + find + "%' ";
        }

        //------------------------------------------------------------------------------------------------------

        public static string ProsesNBR_Partial_BelumProses()
        {
            return "SELECT KODE_DC, NO_NRBSEPIHAK, THN_NRB, PLUID, JUM_NRBPARTIAL FROM DC_NRBPARTIAL_T WHERE FLAG_PROSES IS NULL";
        }

        public static string ProsesNBR_Partial_dataNRB()
        {
            return "SELECT " +
                   "FLAG_PROSES, FLAG_SYNC, KODE_DC, NO_NRBSEPIHAK, THN_NRB, PLUID, JUM_NRBPARTIAL, KETERANGAN " +
                   "FROM DC_NRBPARTIAL_T";
        }

        public static string ProsesNBR_Partial_dataNRB_Partial()
        {
            return "SELECT " +
                   "NO_NRBPARTIAL, COUNT(*) AS Kuantitas " +
                   "FROM DC_NRBPARTIAL_DTL_T " +
                   "GROUP BY NO_NRBPARTIAL";
        }

        public static string FindProsesNBR_Partial_dataNRB(string find)
        {
            return "SELECT " +
                   "FLAG_PROSES, FLAG_SYNC, KODE_DC, NO_NRBSEPIHAK, THN_NRB, PLUID, JUM_NRBPARTIAL, KETERANGAN " +
                   "FROM DC_NRBPARTIAL_T" +
                   "WHERE KODE_DC LIKE '%" + find + "%' OR NO_NRBSEPIHAK LIKE '%" + find + "%' OR PLUID LIKE '%" + find + "%' ";
        }

        //-------------------------------------------------------------------------------------------------------------------------

        public static string Rekapitulasi_BAP_PerSUP(string bulan, string tahun, string namaSupp)
        {
            return "SELECT a.kode_supp AS KODE_SUPP, MAX (a.nama_supp) AS NAMA_SUPP, MAX (a.no_srps) AS NO_SRPS, COUNT(*) PLUID , SUM(qty) AS QTY, SUM (qty * hpp) AS NILAI, MAX (TO_CHAR(tgl_bapsepihak,'dd Month yyyy')) AS TGL_BAPSEPIHAK, " +
                                      "MAX(CASE WHEN TAG IN('N','F','R') THEN '36' END) AS BATAS_WAKTU, MAX(TO_CHAR(CASE WHEN TAG IN('N','F','R') THEN a.TGL_BAPSEPIHAK + 36 END,'dd Month yyyy')) AS TGL_SETELAH_BATAS " +
                                      "FROM DC_SRPS_T a JOIN DC_NRBPARTIAL_T b ON a.PLUID = b.PLUID " +
                                      "WHERE a.nama_supp='" + namaSupp + "' AND TO_CHAR(TGL_NRBSEPIHAK,'FMMonth')='" + bulan + "' AND TO_CHAR(TGL_NRBSEPIHAK,'YYYY')='" + tahun + "'" +
                                      "GROUP BY kode_supp ORDER BY tgl_bapsepihak ASC";
        }

        public static string Rekapitulasi_BAP_ALL(string bulan, string tahun)
        {
            return "SELECT a.kode_supp AS KODE_SUPP, MAX (a.nama_supp) AS NAMA_SUPP, MAX (a.no_srps) AS NO_SRPS, COUNT(*) PLUID , SUM(qty) AS QTY, SUM (qty * hpp) AS NILAI, MAX (TO_CHAR(tgl_bapsepihak,'dd Month yyyy')) AS TGL_BAPSEPIHAK, " +
                                      "MAX(CASE WHEN TAG IN('N','F','R') THEN '36' END) AS BATAS_WAKTU, MAX(TO_CHAR(CASE WHEN TAG IN('N','F','R') THEN a.TGL_BAPSEPIHAK + 36 END,'dd Month yyyy')) AS TGL_SETELAH_BATAS " +
                                      "FROM DC_SRPS_T a JOIN DC_NRBPARTIAL_T b ON a.PLUID = b.PLUID " +
                                      "WHERE TO_CHAR(TGL_NRBSEPIHAK,'FMMonth')='" + bulan + "' AND TO_CHAR(TGL_NRBSEPIHAK,'YYYY')='" + tahun + "'" +
                                      "GROUP BY kode_supp ORDER BY tgl_bapsepihak ASC";
        }

        public static string Rekapitulasi_NRB_PerSUP(string bulan, string tahun, string namaSupp)
        {
            return "SELECT a.kode_supp AS KODE_SUPP, MAX (a.nama_supp) AS NAMA_SUPP, MAX (a.no_srps) AS NO_SRPS, COUNT(*) PLUID, SUM(qty) AS QTY, SUM (qty * hpp) AS NILAI, " +
                                      "MAX (TO_CHAR(tgl_nrbsepihak,'dd Month yyyy')) AS TGL_NRBSEPIHAK, MAX (TO_CHAR(tanggal,'dd Month yyyy')) AS TGL_PENGAJUAN, SUM (jum_nrbpartial) AS JUMLAH_NRB " +
                                      "FROM DC_SRPS_T a JOIN DC_NRBPARTIAL_T b ON a.no_srps = b.no_srps " +
                                      "JOIN DC_NRBPARTIAL_DTL_T c ON b.no_nrbsepihak = c.no_nrbsepihak WHERE a.nama_supp='" + namaSupp + "' AND TO_CHAR(TGL_NRBSEPIHAK,'FMMonth')='" + bulan + "' AND TO_CHAR(TGL_NRBSEPIHAK,'YYYY')='" + tahun + "'" +
                                      "GROUP BY kode_supp ORDER BY tgl_nrbsepihak ASC";
        }

        public static string Rekapitulasi_NRB_ALL(string bulan, string tahun)
        {
            return "SELECT a.kode_supp, MAX (a.nama_supp) AS NAMA_SUPP, MAX (a.no_srps) AS NO_SRPS, COUNT(*) pluid, SUM(qty) AS QTY, SUM (qty * hpp) AS Nilai, " +
                                      "MAX (TO_CHAR(tgl_nrbsepihak,'dd Month yyyy')) AS tgl_nrbsepihak, MAX (TO_CHAR(tanggal,'dd Month yyyy')) AS tgl_pengajuan, SUM (jum_nrbpartial) AS JUMLAH_NRB " +
                                      "FROM DC_SRPS_T a JOIN DC_NRBPARTIAL_T b ON a.no_srps = b.no_srps " +
                                      "JOIN DC_NRBPARTIAL_DTL_T c ON b.no_nrbsepihak = c.no_nrbsepihak WHERE TO_CHAR(TGL_NRBSEPIHAK,'FMMonth')='" + bulan + "' AND TO_CHAR(TGL_NRBSEPIHAK,'YYYY')='" + tahun + "'" +
                                      "GROUP BY kode_supp ORDER BY tgl_nrbsepihak ASC";
        }


    }
}
