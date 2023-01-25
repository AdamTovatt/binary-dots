using System.Collections;
using System.Text;

namespace BinaryDots
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            pictureBox1.Image = GetDotsAsImage(GetDots(text), 18, 50, -5, 100, Color.Black, Color.White);
            pictureBox2.Image = GetDotsAsImage(GetDots(text), 18, 50, -5, 100, Color.White, Color.Black);

            pictureBox1.Image.Save("forflutna.png");
        }

        private bool[] GetDots(string text)
        {
            List<bool> result = new List<bool>();
            
            foreach(byte character in ASCIIEncoding.ASCII.GetBytes(text, 0, text.Length))
            {
                BitArray bits = new BitArray(new byte[] { character });

                List<bool> thisByte = new List<bool>();
                foreach(bool b in bits)
                {
                    thisByte.Add(b);
                }
                thisByte.Reverse();
                result.AddRange(thisByte);
            }

            //result.Reverse();
            return result.ToArray();
        }

        private Bitmap GetDotsAsImage(bool[] dots, int diameter, int edgeMargin, int spacing, double distanceCutoff, Color color1, Color color2)
        {
            Bitmap bmp = new Bitmap((diameter * 2 + spacing) * dots.Length + edgeMargin, diameter * 2 + edgeMargin);

            for(int i = 0; i < dots.Length; i++)
            {
                int xCenter = edgeMargin / 2 + i * ((diameter * 2) + spacing);
                int yCenter = edgeMargin / 2 + diameter;

                for (int x = 0; x < diameter * 2; x++)
                {
                    for (int y = 0; y < diameter * 2; y++)
                    {
                        double distance = Math.Pow(x - (diameter / 2.0), 2) + Math.Pow(y - (diameter / 2.0), 2);

                        if(distance < distanceCutoff)
                        {
                            bmp.SetPixel(xCenter + x + (int)Math.Round(diameter / 2.0), yCenter + y + (int)Math.Round(diameter / 2.0), dots[i] ? color1 : color2);
                        }
                    }
                }
            }

            return bmp;
        }
    }
}