using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace VoiceAssistant
{
    public static class Manager
    {
        public static readonly string[] grammerCommands = File.ReadAllLines(Environment.CurrentDirectory + "//CommandsFolder//commands.txt");
        public static readonly string[] grammerListening = File.ReadAllLines(Environment.CurrentDirectory + "//CommandsFolder//listeningcommand.txt");
        public static SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine();
        public static SpeechRecognitionEngine listeningEngine = new SpeechRecognitionEngine();
        public static SpeechSynthesizer assistant = new SpeechSynthesizer();
        public static StringBuilder speechSB = new StringBuilder();
        public static System.Media.SoundPlayer music = new System.Media.SoundPlayer();
        public static Random random = new Random();
        
    }
}
