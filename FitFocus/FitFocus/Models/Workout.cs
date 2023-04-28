using System;
using System.Collections.Generic;

namespace FitFocus.Models
{
    public class Workout
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string approximate_duration { get; set; }
        public string cover_image_url { get; set; }
        public List<Serie> Series { get; set; }

        public string SeriesNumber
        {
            get
            {
                return Series.Count.ToString();
            }
        }

        public Workout()
        {

        }

        public Workout(string name, string approxTime, string coverImageUrl, List<Serie> series = null)
        {
            if (series is null) series = new List<Serie>();

            //this.Id = new Guid().ToString();
            this.name = name;
            this.approximate_duration = approxTime;
            this.cover_image_url = coverImageUrl;
            this.Series = series;
        }
    }

    public class Serie
    {
        public string id;
        public string name;
        public int sets_number;
        public string time_between_sets;
        public string time_after_sets;
        public Excercise excercise;

        public Serie()
        {

        }

        public Serie(string name, int numberOfSets, string TimeBetweenSets, string TimeAfterSets, Excercise excercise)
        {
            this.name = name;
            this.sets_number = numberOfSets;
            this.time_between_sets = TimeBetweenSets;
            this.time_after_sets = TimeAfterSets;
            this.excercise = excercise;
        }
    }

    public class Excercise
    {
        public string id;
        public string name;
        public string repetitions;
        public string path_to_cover_image;
        public string path_to_video;

        public Excercise()
        {

        }

        public Excercise(string name, string numberOfRepetitions)
        {
            this.name = name;
            this.repetitions = numberOfRepetitions;
        }
    }

    public class WorkoutResponse
    {
        public bool success;
        public Workout[] data;
        public string message;
    }
}
