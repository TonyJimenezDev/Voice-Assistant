using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

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
            Manager.assistant.SelectVoiceByHints(VoiceGender.Male);
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
            Manager.speechSB.Append(e.Result.Text);

            // Notify the assistant and gets it speaking again
            if(Manager.speechSB.ToString() == "Logan")
            {
                Manager.listeningEngine.RecognizeAsyncCancel(); // Turn off
                Manager.assistant.SpeakAsync("Yes?");
                Manager.recognitionEngine.RecognizeAsync(RecognizeMode.Multiple); // Turn on
            }
            Manager.speechSB.Clear();
        }

        // Assistant becomes alert on any speech, kind of buggy - resets timer
        private void Recognizer_SpeechRecognize(object sender, SpeechDetectedEventArgs e)
        {
            listeningTimeOut = 0;
            speaking_Timer.Start();
        }

        private void RecognitionEngine_SpeechRecognize(object sender, SpeechRecognizedEventArgs e)
        {
            
            // Bulk of the work TODO
            Manager.speechSB.Append(e.Result.Text);
            assistantCommands.Commands(Manager.speechSB, this);


            Manager.assistant.SpeakAsync(Manager.speechSB.ToString());
            
            label2.Text = Manager.speechSB.ToString();
            Manager.speechSB.Clear();
        }

        // Handle assistant idle time
        private void Speaking_Timer_Tick(object sender, EventArgs e)
        {
            listeningTimeOut += 1;
            switch (listeningTimeOut)
            {
                case 1:
                    Manager.recognitionEngine.RecognizeAsyncCancel();
                    break;
                case 2:
                    speaking_Timer.Stop();
                    Manager.listeningEngine.RecognizeAsync(RecognizeMode.Multiple);
                    listeningTimeOut = 0;
                    break;
            }
            
        }
    }
}
