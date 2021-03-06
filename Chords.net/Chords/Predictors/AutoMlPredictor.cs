﻿using Chords.Entities;
using Chords.MachineLearning;
using Chords.Profiling;
using Microsoft.ML;
using System;

namespace Chords.Predictors
{
    public class AutoMlPredictor : IPredictor
    {
        private readonly PredictionEngine<ChordData, ChordPredictionResult>
            engine;

        public AutoMlPredictor(string filePath = "./models/model1595137632S120L0.004830031641869656.model")
        {
            var predictionPipeline = AutoMlModelCreation
                .MlContextInstance.Model.Load(
                    filePath, out _);

            engine = AutoMlModelCreation.MlContextInstance.Model
                .CreatePredictionEngine<ChordData, ChordPredictionResult>(
                    predictionPipeline);
        }

        public string GetPrediction(float[] sample, int sampleRate)
        {
            var pcp =
                Profiling.Profiling.PitchClassProfileForSamples(sample,
                    sampleRate);

            var chordData = AutoMlModelCreation.GetChordDataFromPcp(pcp);

            var prediction = engine.Predict(chordData).ChordPrediction;

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(prediction.ToLower());
        }

        public Chord GetPredictionWithChord(float[] sample, int sampleRate)
        {
            var pcp =
                Profiling.Profiling.PitchClassProfileForSamples(sample,
                    sampleRate);

            var chordData = AutoMlModelCreation.GetChordDataFromPcp(pcp);

            var prediction = engine.Predict(chordData).ChordPrediction;

            var chord = new Chord(sample, sampleRate, System.Globalization.CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(prediction.ToLower()), pcp);
            return chord;
        }

        public string[] GetPredictions(float[] samples, int sampleRate,
            int windowInMs,
            IProgress<int> progress)
        {
            return LongAudioProfiling
                .PredictionWithProgressReportAndCustomPrediction(sampleRate,
                    samples, windowInMs, progress,
                    (sampleRateLambda, samplesLambda) =>
                        GetPrediction(samplesLambda, sampleRateLambda));
        }

        public string[] GetPredictionForFile(string filePath,
            IProgress<int> progress,
            int windowInMs)
        {
            var (sampleRate, samples) = Chords.Profiling.Profiling.GetSamples(filePath);
            return GetPredictions(samples, sampleRate, windowInMs, progress);
        }

        public Chord[] GetPredictionsWithChords(float[] samples, int sampleRate, int windowInMs, IProgress<int> progress)
        {
            return LongAudioProfiling
                .PredictionWithProgressReportAndCustomPredictionWithChords(sampleRate,
                    samples, windowInMs, progress,
                    (sampleRateLambda, samplesLambda) =>
                        GetPredictionWithChord(samplesLambda, sampleRateLambda));
        }

        public Chord[] GetPredictionWithBorderDetection(float[] samples, int sampleRate, int windowSizeInMs, int offsetInMs, IProgress<int> progress)
        {
            return LongAudioProfiling
                .PredictionWithBorderDetection(sampleRate,
                    samples, windowSizeInMs, offsetInMs, progress,
                    (sampleRateLambda, samplesLambda) =>
                        GetPredictionWithChord(samplesLambda, sampleRateLambda));
        }
    }
}
