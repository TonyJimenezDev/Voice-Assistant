using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace VoiceAssistant
{
    public partial class Form1 : Form
    {
        AssistantCommands assistantCommands = new AssistantCommands();
        int listeningTimeOut = 0;
        

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SetupRecognitionEngine();
            SetupListeningEngine();
            speaking_Timer.Start();
        }

        private void SetupRecognitionEngine()
        {
            Choices choices = new Choices();
            choices.Add(Manager.grammerCommands);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));

            Manager.recognitionEngine.SetInputToDefaultAudioDevice();
            Manager.recognitionEngine.LoadGrammarAsync(grammar);
            Manager.recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(RecognitionEngine_SpeechRecognize);
            Manager.recognitionEngine.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(Recognizer_SpeechRecognize);
            Manager.recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            ReadOnlyCollection<InstalledVoice> getVoices = Manager.assistant.GetInstalledVoices();
            Manager.assistant.SelectVoiceByHints(VoiceGender.Male);           
            foreach(InstalledVoice voice in getVoices)
            {
                if(voice.VoiceInfo.Name == "Microsoft James")
                {
                    Manager.assistant.SelectVoice("Microsoft James");
                    Manager.assistant.Rate = 02;
                    break;
                }
            }   
        }

        // Setup ListeningEngine
        private void SetupListeningEngine()
        {
            Choices choices = new Choices();
            choices.Add(Manager.grammerListening);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));

            Manager.listeningEngine.SetInputToDefaultAudioDevice();
            Manager.listeningEngine.LoadGrammarAsync(grammar);
            Manager.listeningEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(StartListening_SpeechRecognize);
        }

        private void StartListening_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {
            Manager.speechSB.Clear();
            Manager.speechSB.Append(e.Result.Text);
            
            // Notify the assistant and gets it speaking again
            if(Manager.speechSB.ToString() == "Logan.")
            {
                Manager.assistant.SpeakAsync("Whats up?"); // TODO: Check into.
                Manager.listeningEngine.RecognizeAsyncCancel(); // Turn off
                Manager.recognitionEngine.RecognizeAsync(RecognizeMode.Multiple); // Turn on
                speaking_Timer.Start();
                listeningTimeOut = 0;
            }
            Manager.speechSB.Clear();
        }

        // Assistant becomes alert on any speech, kind of buggy - resets timer TODO: Add text to speech
        private void Recognizer_SpeechRecognize(object sender, SpeechDetectedEventArgs e)
        {
            speaking_Timer.Start();
            listeningTimeOut = 0;
        }

        private void RecognitionEngine_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {
            // Bulk of the work TODO
            if(e.Result.Confidence > .75 && !e.Result.Equals(Manager.speechSB.ToString()))
            {
                
                Manager.speechSB.Append(e.Result.Text);
                assistantCommands.Commands(Manager.speechSB, this);

                //Manager.speechBuilder.AppendText(assistantCommands.Commands(Manager.speechBuilder, this).ToString());
                Manager.assistant.SpeakAsync(Manager.speechSB.ToString());

                label2.Text = Manager.speechSB.ToString();
            }
            Manager.speechSB.Clear();
        }

        // Handle assistant idle time
        private void Speaking_Timer_Tick(object sender, EventArgs e)
        {
            listeningTimeOut += 1;

            if(listeningTimeOut == 10) Manager.recognitionEngine.RecognizeAsyncCancel(); // Turn Off 
            else if(listeningTimeOut == 11)
            {
                // Notify that Assistant is going away
                Manager.speechSB.Append("Going to take a coffee break, call me if you need me");
                label2.Text = Manager.speechSB.ToString();
                Manager.assistant.SpeakAsync(Manager.speechSB.ToString());
                Manager.listeningEngine.RecognizeAsync(RecognizeMode.Multiple); // Turn on
                speaking_Timer.Stop();
                listeningTimeOut = 0;
                Manager.speechSB.Clear();
            }            
        }
    }
}
