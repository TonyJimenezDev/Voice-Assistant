using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Speech.Recognition;

using System.IO;
using Microsoft.Speech;

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
            //Manager.assistant.SelectVoiceByHints(VoiceGender.Male);
            var n = Manager.assistant.GetInstalledVoices();
            Manager.assistant.SelectVoice("Microsoft James");
            Manager.assistant.Rate = 02;
            
            //Manager.speechBuilder.AppendText("Hello");
            //Manager.assistant.SpeakAsync("Hello");
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
            Manager.speechSB.Append(e.Result.Text);
            assistantCommands.Commands(Manager.speechSB, this);

            //Manager.speechBuilder.AppendText(assistantCommands.Commands(Manager.speechBuilder, this).ToString());
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
                case 30:
                    Manager.recognitionEngine.RecognizeAsyncCancel();
                    break;
                case 31:
                    Manager.listeningEngine.RecognizeAsync(RecognizeMode.Multiple);
                    speaking_Timer.Stop();
                    listeningTimeOut = 0;
                    break;
            }
            
        }
    }
}
