using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Mercurio.Core
{
    class LogActivity
    {
        private string nomeDoArquivo = string.Empty;
        private string nomeDoModulo = string.Empty;
        private LogNivel logNivel;

        public LogActivity(LogNivel level, string nomeDoModulo)
        {
            string nomeFormatado = nomeDoModulo.Replace(" ", "");
            this.nomeDoModulo = nomeFormatado;
            string directory = Directory.GetCurrentDirectory();
            directory = directory + @"\LOGS";
            if (!Directory.Exists(directory))
            {
                DirectoryInfo di = Directory.CreateDirectory(directory);
                return;
            }
            this.nomeDoArquivo = Path.Combine(directory, string.Format("{0}_{1}.txt", this.nomeDoModulo, DateTime.Now.ToString("dd-MM-yyyy")));
            logNivel = level;

        }

        public bool Write(LogNivel nivelLog, string texto)
        {
            try
            {
                string nivel = string.Empty;
                if (logNivel == LogNivel.Verbose || (int)logNivel >= (int)nivelLog)
                {
                    switch (nivelLog)
                    {
                        case LogNivel.Erro:
                            nivel = "Erro";
                            break;
                        case LogNivel.Warning:
                            nivel = "Warning";
                            break;
                        case LogNivel.Info:
                            nivel = "Info";
                            break;
                        case LogNivel.Comment:
                            nivel = "Comment";
                            break;
                    }

                    string textoFormatado = string.Format("[{0}]: {1}- {2}", nivel, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), texto);

                    using (StreamWriter objEscrita = new StreamWriter(nomeDoArquivo, true))
                    {
                        objEscrita.WriteLine(textoFormatado);
                        objEscrita.Close();
                    }


                }


                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[LogActivity]: {0}", ex));
                return false;
            }

        }

        public bool Write(LogNivel nivelLog, Exception ex)
        {
            try
            {
                string nivel = string.Empty;
                if (logNivel == LogNivel.Verbose || (int)logNivel >= (int)nivelLog)
                {
                    switch (nivelLog)
                    {
                        case LogNivel.Erro:
                            nivel = "Erro";
                            break;
                        case LogNivel.Warning:
                            nivel = "Warning";
                            break;
                        case LogNivel.Info:
                            nivel = "Info";
                            break;
                        case LogNivel.Comment:
                            nivel = "Comment";
                            break;
                    }

                    string textoFormatado = string.Format("[{0}]: {1}- {2}", nivel, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), ex);

                    using (StreamWriter objEscrita = new StreamWriter(nomeDoArquivo, true))
                    {
                        objEscrita.WriteLine(textoFormatado);
                        objEscrita.Close();
                    }

                }


                return true;
            }

            catch (Exception exception)
            {
                Console.WriteLine(string.Format("[LogActivity]: {0}", exception));
                return false;
            }

        }
    }
}
