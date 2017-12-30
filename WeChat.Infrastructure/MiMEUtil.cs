using System;
using System.Collections;

namespace WeChat.Infrastructure
{
    /// <summary>
    /// MIME类型
    /// </summary>
    public static class MiMEUtil
    {
        // 通过自己定义一个静态类
        // 将所有的Content Type都扔进去吧
        // 调用的时候直接调用静态方法即可。

        private static Hashtable _mimeMappingTable;

        private static void AddMimeMapping(string extension, string MimeType)
        {
            MiMEUtil._mimeMappingTable.Add(extension, MimeType);
        }
        /// <summary>
        /// 根据文件名获取MIME 类型
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetMimeMapping(string FileName)
        {
            string text = null;
            int num = FileName.LastIndexOf('.');
            if (0 < num && num > FileName.LastIndexOf('\\'))
            {
                text = (string)MiMEUtil._mimeMappingTable[FileName.Substring(num)];
            }
            if (text == null)
            {
                text = (string)MiMEUtil._mimeMappingTable[".*"];
            }
            return text;
        }

        static MiMEUtil()
        {
            MiMEUtil._mimeMappingTable = new Hashtable(190, StringComparer.CurrentCultureIgnoreCase);
            MiMEUtil.AddMimeMapping(".323", "text/h323");
            MiMEUtil.AddMimeMapping(".asx", "video/x-ms-asf");
            MiMEUtil.AddMimeMapping(".acx", "application/internet-property-stream");
            MiMEUtil.AddMimeMapping(".ai", "application/postscript");
            MiMEUtil.AddMimeMapping(".aif", "audio/x-aiff");
            MiMEUtil.AddMimeMapping(".aiff", "audio/aiff");
            MiMEUtil.AddMimeMapping(".axs", "application/olescript");
            MiMEUtil.AddMimeMapping(".aifc", "audio/aiff");
            MiMEUtil.AddMimeMapping(".asr", "video/x-ms-asf");
            MiMEUtil.AddMimeMapping(".avi", "video/x-msvideo");
            MiMEUtil.AddMimeMapping(".asf", "video/x-ms-asf");
            MiMEUtil.AddMimeMapping(".au", "audio/basic");
            MiMEUtil.AddMimeMapping(".application", "application/x-ms-application");
            MiMEUtil.AddMimeMapping(".bin", "application/octet-stream");
            MiMEUtil.AddMimeMapping(".bas", "text/plain");
            MiMEUtil.AddMimeMapping(".bcpio", "application/x-bcpio");
            MiMEUtil.AddMimeMapping(".bmp", "image/bmp");
            MiMEUtil.AddMimeMapping(".cdf", "application/x-cdf");
            MiMEUtil.AddMimeMapping(".cat", "application/vndms-pkiseccat");
            MiMEUtil.AddMimeMapping(".crt", "application/x-x509-ca-cert");
            MiMEUtil.AddMimeMapping(".c", "text/plain");
            MiMEUtil.AddMimeMapping(".css", "text/css");
            MiMEUtil.AddMimeMapping(".cer", "application/x-x509-ca-cert");
            MiMEUtil.AddMimeMapping(".crl", "application/pkix-crl");
            MiMEUtil.AddMimeMapping(".cmx", "image/x-cmx");
            MiMEUtil.AddMimeMapping(".csh", "application/x-csh");
            MiMEUtil.AddMimeMapping(".cod", "image/cis-cod");
            MiMEUtil.AddMimeMapping(".cpio", "application/x-cpio");
            MiMEUtil.AddMimeMapping(".clp", "application/x-msclip");
            MiMEUtil.AddMimeMapping(".crd", "application/x-mscardfile");
            MiMEUtil.AddMimeMapping(".deploy", "application/octet-stream");
            MiMEUtil.AddMimeMapping(".dll", "application/x-msdownload");
            MiMEUtil.AddMimeMapping(".dot", "application/msword");
            MiMEUtil.AddMimeMapping(".doc", "application/msword");
            MiMEUtil.AddMimeMapping(".dvi", "application/x-dvi");
            MiMEUtil.AddMimeMapping(".dir", "application/x-director");
            MiMEUtil.AddMimeMapping(".dxr", "application/x-director");
            MiMEUtil.AddMimeMapping(".der", "application/x-x509-ca-cert");
            MiMEUtil.AddMimeMapping(".dib", "image/bmp");
            MiMEUtil.AddMimeMapping(".dcr", "application/x-director");
            MiMEUtil.AddMimeMapping(".disco", "text/xml");
            MiMEUtil.AddMimeMapping(".exe", "application/octet-stream");
            MiMEUtil.AddMimeMapping(".etx", "text/x-setext");
            MiMEUtil.AddMimeMapping(".evy", "application/envoy");
            MiMEUtil.AddMimeMapping(".eml", "message/rfc822");
            MiMEUtil.AddMimeMapping(".eps", "application/postscript");
            MiMEUtil.AddMimeMapping(".flr", "x-world/x-vrml");
            MiMEUtil.AddMimeMapping(".fif", "application/fractals");
            MiMEUtil.AddMimeMapping(".gtar", "application/x-gtar");
            MiMEUtil.AddMimeMapping(".gif", "image/gif");
            MiMEUtil.AddMimeMapping(".gz", "application/x-gzip");
            MiMEUtil.AddMimeMapping(".hta", "application/hta");
            MiMEUtil.AddMimeMapping(".htc", "text/x-component");
            MiMEUtil.AddMimeMapping(".htt", "text/webviewhtml");
            MiMEUtil.AddMimeMapping(".h", "text/plain");
            MiMEUtil.AddMimeMapping(".hdf", "application/x-hdf");
            MiMEUtil.AddMimeMapping(".hlp", "application/winhlp");
            MiMEUtil.AddMimeMapping(".html", "text/html");
            MiMEUtil.AddMimeMapping(".htm", "text/html");
            MiMEUtil.AddMimeMapping(".hqx", "application/mac-binhex40");
            MiMEUtil.AddMimeMapping(".isp", "application/x-internet-signup");
            MiMEUtil.AddMimeMapping(".iii", "application/x-iphone");
            MiMEUtil.AddMimeMapping(".ief", "image/ief");
            MiMEUtil.AddMimeMapping(".ivf", "video/x-ivf");
            MiMEUtil.AddMimeMapping(".ins", "application/x-internet-signup");
            MiMEUtil.AddMimeMapping(".ico", "image/x-icon");
            MiMEUtil.AddMimeMapping(".jpg", "image/jpeg");
            MiMEUtil.AddMimeMapping(".jfif", "image/pjpeg");
            MiMEUtil.AddMimeMapping(".jpe", "image/jpeg");
            MiMEUtil.AddMimeMapping(".jpeg", "image/jpeg");
            MiMEUtil.AddMimeMapping(".js", "application/x-javascript");
            MiMEUtil.AddMimeMapping(".lsx", "video/x-la-asf");
            MiMEUtil.AddMimeMapping(".latex", "application/x-latex");
            MiMEUtil.AddMimeMapping(".lsf", "video/x-la-asf");
            MiMEUtil.AddMimeMapping(".manifest", "application/x-ms-manifest");
            MiMEUtil.AddMimeMapping(".mhtml", "message/rfc822");
            MiMEUtil.AddMimeMapping(".mny", "application/x-msmoney");
            MiMEUtil.AddMimeMapping(".mht", "message/rfc822");
            MiMEUtil.AddMimeMapping(".mid", "audio/mid");
            MiMEUtil.AddMimeMapping(".mpv2", "video/mpeg");
            MiMEUtil.AddMimeMapping(".man", "application/x-troff-man");
            MiMEUtil.AddMimeMapping(".mvb", "application/x-msmediaview");
            MiMEUtil.AddMimeMapping(".mpeg", "video/mpeg");
            MiMEUtil.AddMimeMapping(".m3u", "audio/x-mpegurl");
            MiMEUtil.AddMimeMapping(".mdb", "application/x-msaccess");
            MiMEUtil.AddMimeMapping(".mpp", "application/vnd.ms-project");
            MiMEUtil.AddMimeMapping(".m1v", "video/mpeg");
            MiMEUtil.AddMimeMapping(".mpa", "video/mpeg");
            MiMEUtil.AddMimeMapping(".me", "application/x-troff-me");
            MiMEUtil.AddMimeMapping(".m13", "application/x-msmediaview");
            MiMEUtil.AddMimeMapping(".movie", "video/x-sgi-movie");
            MiMEUtil.AddMimeMapping(".m14", "application/x-msmediaview");
            MiMEUtil.AddMimeMapping(".mpe", "video/mpeg");
            MiMEUtil.AddMimeMapping(".mp2", "video/mpeg");
            MiMEUtil.AddMimeMapping(".mov", "video/quicktime");
            MiMEUtil.AddMimeMapping(".mp3", "audio/mpeg");
            MiMEUtil.AddMimeMapping(".mpg", "video/mpeg");
            MiMEUtil.AddMimeMapping(".ms", "application/x-troff-ms");
            MiMEUtil.AddMimeMapping(".nc", "application/x-netcdf");
            MiMEUtil.AddMimeMapping(".nws", "message/rfc822");
            MiMEUtil.AddMimeMapping(".oda", "application/oda");
            MiMEUtil.AddMimeMapping(".ods", "application/oleobject");
            MiMEUtil.AddMimeMapping(".pmc", "application/x-perfmon");
            MiMEUtil.AddMimeMapping(".p7r", "application/x-pkcs7-certreqresp");
            MiMEUtil.AddMimeMapping(".p7b", "application/x-pkcs7-certificates");
            MiMEUtil.AddMimeMapping(".p7s", "application/pkcs7-signature");
            MiMEUtil.AddMimeMapping(".pmw", "application/x-perfmon");
            MiMEUtil.AddMimeMapping(".ps", "application/postscript");
            MiMEUtil.AddMimeMapping(".p7c", "application/pkcs7-mime");
            MiMEUtil.AddMimeMapping(".pbm", "image/x-portable-bitmap");
            MiMEUtil.AddMimeMapping(".ppm", "image/x-portable-pixmap");
            MiMEUtil.AddMimeMapping(".pub", "application/x-mspublisher");
            MiMEUtil.AddMimeMapping(".pnm", "image/x-portable-anymap");
            MiMEUtil.AddMimeMapping(".png", "image/png");
            MiMEUtil.AddMimeMapping(".pml", "application/x-perfmon");
            MiMEUtil.AddMimeMapping(".p10", "application/pkcs10");
            MiMEUtil.AddMimeMapping(".pfx", "application/x-pkcs12");
            MiMEUtil.AddMimeMapping(".p12", "application/x-pkcs12");
            MiMEUtil.AddMimeMapping(".pdf", "application/pdf");
            MiMEUtil.AddMimeMapping(".pps", "application/vnd.ms-powerpoint");
            MiMEUtil.AddMimeMapping(".p7m", "application/pkcs7-mime");
            MiMEUtil.AddMimeMapping(".pko", "application/vndms-pkipko");
            MiMEUtil.AddMimeMapping(".ppt", "application/vnd.ms-powerpoint");
            MiMEUtil.AddMimeMapping(".pmr", "application/x-perfmon");
            MiMEUtil.AddMimeMapping(".pma", "application/x-perfmon");
            MiMEUtil.AddMimeMapping(".pot", "application/vnd.ms-powerpoint");
            MiMEUtil.AddMimeMapping(".prf", "application/pics-rules");
            MiMEUtil.AddMimeMapping(".pgm", "image/x-portable-graymap");
            MiMEUtil.AddMimeMapping(".qt", "video/quicktime");
            MiMEUtil.AddMimeMapping(".ra", "audio/x-pn-realaudio");
            MiMEUtil.AddMimeMapping(".rgb", "image/x-rgb");
            MiMEUtil.AddMimeMapping(".ram", "audio/x-pn-realaudio");
            MiMEUtil.AddMimeMapping(".rmi", "audio/mid");
            MiMEUtil.AddMimeMapping(".ras", "image/x-cmu-raster");
            MiMEUtil.AddMimeMapping(".roff", "application/x-troff");
            MiMEUtil.AddMimeMapping(".rtf", "application/rtf");
            MiMEUtil.AddMimeMapping(".rtx", "text/richtext");
            MiMEUtil.AddMimeMapping(".sv4crc", "application/x-sv4crc");
            MiMEUtil.AddMimeMapping(".spc", "application/x-pkcs7-certificates");
            MiMEUtil.AddMimeMapping(".setreg", "application/set-registration-initiation");
            MiMEUtil.AddMimeMapping(".snd", "audio/basic");
            MiMEUtil.AddMimeMapping(".stl", "application/vndms-pkistl");
            MiMEUtil.AddMimeMapping(".setpay", "application/set-payment-initiation");
            MiMEUtil.AddMimeMapping(".stm", "text/html");
            MiMEUtil.AddMimeMapping(".shar", "application/x-shar");
            MiMEUtil.AddMimeMapping(".sh", "application/x-sh");
            MiMEUtil.AddMimeMapping(".sit", "application/x-stuffit");
            MiMEUtil.AddMimeMapping(".spl", "application/futuresplash");
            MiMEUtil.AddMimeMapping(".sct", "text/scriptlet");
            MiMEUtil.AddMimeMapping(".scd", "application/x-msschedule");
            MiMEUtil.AddMimeMapping(".sst", "application/vndms-pkicertstore");
            MiMEUtil.AddMimeMapping(".src", "application/x-wais-source");
            MiMEUtil.AddMimeMapping(".sv4cpio", "application/x-sv4cpio");
            MiMEUtil.AddMimeMapping(".tex", "application/x-tex");
            MiMEUtil.AddMimeMapping(".tgz", "application/x-compressed");
            MiMEUtil.AddMimeMapping(".t", "application/x-troff");
            MiMEUtil.AddMimeMapping(".tar", "application/x-tar");
            MiMEUtil.AddMimeMapping(".tr", "application/x-troff");
            MiMEUtil.AddMimeMapping(".tif", "image/tiff");
            MiMEUtil.AddMimeMapping(".txt", "text/plain");
            MiMEUtil.AddMimeMapping(".texinfo", "application/x-texinfo");
            MiMEUtil.AddMimeMapping(".trm", "application/x-msterminal");
            MiMEUtil.AddMimeMapping(".tiff", "image/tiff");
            MiMEUtil.AddMimeMapping(".tcl", "application/x-tcl");
            MiMEUtil.AddMimeMapping(".texi", "application/x-texinfo");
            MiMEUtil.AddMimeMapping(".tsv", "text/tab-separated-values");
            MiMEUtil.AddMimeMapping(".ustar", "application/x-ustar");
            MiMEUtil.AddMimeMapping(".uls", "text/iuls");
            MiMEUtil.AddMimeMapping(".vcf", "text/x-vcard");
            MiMEUtil.AddMimeMapping(".wps", "application/vnd.ms-works");
            MiMEUtil.AddMimeMapping(".wav", "audio/wav");
            MiMEUtil.AddMimeMapping(".wrz", "x-world/x-vrml");
            MiMEUtil.AddMimeMapping(".wri", "application/x-mswrite");
            MiMEUtil.AddMimeMapping(".wks", "application/vnd.ms-works");
            MiMEUtil.AddMimeMapping(".wmf", "application/x-msmetafile");
            MiMEUtil.AddMimeMapping(".wcm", "application/vnd.ms-works");
            MiMEUtil.AddMimeMapping(".wrl", "x-world/x-vrml");
            MiMEUtil.AddMimeMapping(".wdb", "application/vnd.ms-works");
            MiMEUtil.AddMimeMapping(".wsdl", "text/xml");
            MiMEUtil.AddMimeMapping(".xap", "application/x-silverlight-app");
            MiMEUtil.AddMimeMapping(".xml", "text/xml");
            MiMEUtil.AddMimeMapping(".xlm", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xaf", "x-world/x-vrml");
            MiMEUtil.AddMimeMapping(".xla", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xls", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xof", "x-world/x-vrml");
            MiMEUtil.AddMimeMapping(".xlt", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xlc", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xsl", "text/xml");
            MiMEUtil.AddMimeMapping(".xbm", "image/x-xbitmap");
            MiMEUtil.AddMimeMapping(".xlw", "application/vnd.ms-excel");
            MiMEUtil.AddMimeMapping(".xpm", "image/x-xpixmap");
            MiMEUtil.AddMimeMapping(".xwd", "image/x-xwindowdump");
            MiMEUtil.AddMimeMapping(".xsd", "text/xml");
            MiMEUtil.AddMimeMapping(".z", "application/x-compress");
            MiMEUtil.AddMimeMapping(".zip", "application/x-zip-compressed");
            MiMEUtil.AddMimeMapping(".*", "application/octet-stream");
        }
    }

}
