using System;
using System.Text;

namespace VoiceAssistant
{
    public class AssistantCommands
    {
        public AssistantCommands()
        {
        }

        public StringBuilder Commands(StringBuilder text, Form1 _form1)
        {
            // Figure out how to use a dictionary for this.
            //Form1.commandDictionary.Add("Hello", "Hello, my name is Ozzie. How can I help you?");
            //Form1.speech.Append
            switch (Convert.ToString(text))
            {
                case "Logan.":
                    text.Clear();
                    Manager.assistant.SpeakAsyncCancelAll();
                    text.Append("Yes?");
                    break;
                case "What time is it?":
                    text.Clear();
                    text.Append("It is currently " + DateTime.Now.ToShortTimeString());
                    break;
                case "Open Google.":
                    text.Clear();
                    System.Diagnostics.Process.Start("https://www.google.com");
                    text.Append("Opening Google");
                    break;
                case "Close Google.":
                    text.Clear();
                    System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("chrome");
                    foreach (System.Diagnostics.Process process in close) process.Kill();
                    text.Append("Closing Chrome");
                    break;
                case "Faded.":
                    text.Clear();
                    Manager.music.SoundLocation = "faded.wav";
                    Manager.music.Play();
                    text.Append("");
                    break;
                case "Stop.":
                    text.Clear();
                    int rndResponse;
                    Manager.assistant.SpeakAsyncCancelAll();
                    Manager.music.Stop();
                    rndResponse = Manager.random.Next(1,2);
                    if (rndResponse == 1) text.Append("Okay. If you need me just ask");
                    else if (rndResponse == 2) text.Append("Will do. If you need me just ask");
                    Manager.recognitionEngine.RecognizeAsyncCancel(); // Turn off
                    Manager.listeningEngine.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple); // Turn on
                    break;
                case "Show commands.":
                    text.Clear();
                    _form1.showCommands_lstBx.Items.Clear();
                    _form1.showCommands_lstBx.SelectionMode = System.Windows.Forms.SelectionMode.None;
                    _form1.showCommands_lstBx.Visible = true;
                    foreach (string commands in Manager.grammerCommands) _form1.showCommands_lstBx.Items.Add(commands);
                    break;
                case "Hide commands.":
                    text.Clear();
                    _form1.showCommands_lstBx.Visible = false;
                    break;
                case "What's your favorite color?":
                    text.Clear();
                    text.Append("My favorite color is blue.");
                    break;
                default:
                    text.Clear();
                    text.Append("Should I repeat that for you?");
                    // Branch this out to answer the yes or no
                    break;
            }
            return text;
        }
    }
}
