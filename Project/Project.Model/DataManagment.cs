using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    using System.Linq;

    public class DataManagement
    {
        public List<Client> Clients { get; set; }
        public List<Trainer> Trainers { get; set; }
        public List<Workout> Workouts { get; set; }
        public List<Exercise> Exercises { get; set; }

        public DataManagement()
        {
            Clients = new List<Client>();
            Trainers = new List<Trainer>();
            Workouts = new List<Workout>();
            Exercises = new List<Exercise>();
        }

        public void AddClient(Client client) => Clients.Add(client);
        public void AddTrainer(Trainer trainer) => Trainers.Add(trainer);
        public void AddExercise(Exercise exercise) => Exercises.Add(exercise);
        public void AddWorkout(Workout workout) => Workouts.Add(workout);

        public Client GetClientById(int id)
        {
            return Clients.FirstOrDefault(c => c.Id == id);
        }
        public Exercise GetExerciseById(int id)
        {
            return Exercises.FirstOrDefault(e => e.Id == id);
        }
    }
}
