// Repository:  MediaLibrary
// Author:      Jeff Grissom
// Version:     1.xx

using System;
using NLog.Web;
using System.IO;

namespace MediaLibrary
{
class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.scrubbed.csv";

            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(movieFilePath);

            string choice = "";
            do
            {
                // display choices to user
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info($"User choice: {choice}");

                if (choice == "1")
                {
                    // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title:");
                    // input title
                    movie.title = Console.ReadLine();
                    // verify title is unique
                    if (movieFile.isUniqueTitle(movie.title)){
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit):");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(No genres listed)");
                        }

                        //Enter movie director
                        Console.WriteLine("Enter movie director:");
                        input = Console.ReadLine();
                        movie.director = input;

                        //Enter running time (h:m:s)
                        Console.WriteLine("Enter running time:");
                        input = Console.ReadLine();
                        movie.runningTime = TimeSpan.Parse(input);
                        
                        // add movie
                        movieFile.AddMovie(movie);
                    }
                } else if (choice == "2")
                {
                    // Display All Movies
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program ended");
        }
    }
}
