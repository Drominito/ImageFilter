using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ImageFilter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Width = 640, Height = 640;
        public Bitmap SaveBitmapInformations;
        public Bitmap LoadBitmap;
        public BitmapImage bitmapImage;
        public Color[,] PixelInformations;
        public string Verzeichniss = @"P:\Modellierung\2D\Aseprite\Projekte\Farben\TestFarben\";
        public string Datei = "TextPixels";
        public string DateiFormat = ".png";
        public string Pfad;
        public string CopyBitmapFile;

        public MainWindow()
        {
            Pfad = Verzeichniss + Datei + DateiFormat;
            CopyBitmapFile = $"{Verzeichniss + Datei}_02{DateiFormat}";

            InitializeComponent();

            GenerateImage();

        }

        private async void GenerateImage()
        {
            CopyBitmapInformations();

            await SetPixels();

            BitmapSource LoadBitmapSource = ConvertInToCompatibleFormat();

            ImageFront.Source = LoadBitmapSource;
        }

        private BitmapSource ConvertInToCompatibleFormat()
        {
            BitmapSource LoadBitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                                                    LoadBitmap.GetHbitmap(),
                                                    IntPtr.Zero,
                                                    Int32Rect.Empty,
                                                    BitmapSizeOptions.FromEmptyOptions());
            return LoadBitmapSource;
        }

        async private Task SetPixels()
        {
            LoadBitmap = new Bitmap(Width, Height);

            await Task.Run(() =>
            {

            });


            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color DefaultColor;

                    //if (x + 8 < Width) { DefaultColor = (Color)PixelInformations.GetValue(x + 8, y); }
                    //else { DefaultColor = (Color)PixelInformations.GetValue(x, y); }
                    DefaultColor = (Color)PixelInformations.GetValue(x, y);

                    byte Red = DefaultColor.R;
                    byte Green = DefaultColor.G;
                    byte Blue = DefaultColor.B;

                    double CenterX = Width / 2;
                    double CenterY = Height / 2;
                    double Radius = ( ((Width + Height) / 2) / 4 );

                    double distancesquared = Math.Sqrt(Math.Pow(x - CenterX, 2) + Math.Pow(y - CenterY, 2));
                    distancesquared = distancesquared / (Radius * Radius);

                    if (distancesquared <= Radius / 20 && PixelInformations[x, y].R <= 200 && PixelInformations[x, y].G <= 200 && PixelInformations[x, y].B <= 200)
                    {
                        LoadBitmap.SetPixel(x, y, Color.FromArgb((Red), (Green), (Blue)));
                    }

                    // Das macht ein Muster das was mit einem Kreis zu tun hat, und du sollst ein Übergang zwischen dem Machen, realisiere es im dem du noch das "else if" block löschst oder Kommentierst.
                    // Stelle es wirklich auf Github rein und zwar auf ÖFFENTLICH und mache es unabhängiger mit dem Links und etc, mache ich die Asynkronen Methoden gescheit.

                    //else if (x * x * y * y % 10 == 0)
                    //{
                    //    if (Blue * x <= 255)
                    //    {
                    //        LoadBitmap.SetPixel(x, y, Color.FromArgb((Red), (Green), (Blue * x)));
                    //    }
                    //}

                    else
                    {
                        LoadBitmap.SetPixel(x, y, Color.FromArgb(new Random().Next(Red), new Random().Next(Green), new Random().Next(Blue)));
                        //LoadBitmap.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    

                }
            }
                
                    
                    

                   
                   


            if (File.Exists(CopyBitmapFile))
            {
                File.Delete(CopyBitmapFile);
                LoadBitmap.Save(CopyBitmapFile);
            }
            else
            {
                LoadBitmap.Save(CopyBitmapFile);
            }
        }

      






        private void CopyBitmapInformations()
        {

            BitmapSource bitmapSource = StartUp2();
            GetPixels();

        }

        private BitmapSource StartUp2()
        {
            PixelInformations = new Color[Width, Height];


            Image PfadImage = Image.FromFile(Pfad);
            SaveBitmapInformations = new Bitmap(PfadImage, Width, Height);


            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                                                SaveBitmapInformations.GetHbitmap(),
                                                IntPtr.Zero,
                                                Int32Rect.Empty,
                                                BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;
        }

        private void GetPixels()
        {
            for (int y = 0; y < Width; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    PixelInformations[x, y] = SaveBitmapInformations.GetPixel(x, y);

                }
            }
        }
    }
}
