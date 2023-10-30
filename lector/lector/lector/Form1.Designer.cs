using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Speech.Synthesis;
using System.Reflection.PortableExecutable;

namespace PDFReaderApp
{
    public partial class MainForm : Form
    {
        private string archivoPDF;
        private SpeechSynthesizer synthesizer;
        private bool pausado;

        public MainForm()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            pausado = false;
        }

        private void CargarPDFButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos PDF|*.pdf",
                Title = "Seleccionar archivo PDF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                archivoPDF = openFileDialog.FileName;
                CargarContenidoPDF();
            }
        }

        private void CargarContenidoPDF()
        {
            using (PdfReader pdfReader = new PdfReader(archivoPDF))
            {
                synthesizer.SpeakAsyncCancelAll();
                pausado = false;

                for (int pagina = 1; pagina <= pdfReader.NumberOfPages; pagina++)
                {
                    if (pausado)
                    {
                        synthesizer.SpeakAsyncCancelAll();
                        break;
                    }

                    string contenidoPagina = PdfTextExtractor.GetTextFromPage(pdfReader.GetPage(pagina));
                    TextoLeidoLabel.Text = contenidoPagina;
                    Application.DoEvents(); // Actualizar la interfaz gráfica
                    synthesizer.SpeakAsync(contenidoPagina);
                }
            }
        }

        private void AlternarPausaButton_Click(object sender, EventArgs e)
        {
            pausado = !pausado;

            if (pausado)
            {
                synthesizer.SpeakAsyncCancelAll();
            }
            else
            {
                CargarContenidoPDF();
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
