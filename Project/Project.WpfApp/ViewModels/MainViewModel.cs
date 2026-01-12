using Project.DAL;
using Project.Models;
using Project.Services;
using Project.WpfApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Project.WpfApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDBContext _dbContext;

        private int _indexOfSelectedTab;
        public int IndexOfSelectedTab
        {
            get => _indexOfSelectedTab;
            set
            {
                if (_indexOfSelectedTab != value)
                {
                    _indexOfSelectedTab = value;
                    OnPropertyChanged(nameof(IndexOfSelectedTab));
                }
            }
        }

        private Actor? _selectedActor;
        public Actor? SelectedActor
        {
            get => _selectedActor;
            set
            {
                _selectedActor = value;
                OnPropertyChanged(nameof(SelectedActor));
            }
        }

        private Auditorium? _selectedAuditorium;
        public Auditorium? SelectedAuditorium
        {
            get => _selectedAuditorium;
            set
            {
                _selectedAuditorium = value;
                OnPropertyChanged(nameof(SelectedAuditorium));
            }
        }

        private Cinema? _selectedCinema;
        public Cinema? SelectedCinema
        {
            get => _selectedCinema;
            set
            {
                _selectedCinema = value;
                OnPropertyChanged(nameof(SelectedCinema));
            }
        }

        private Film? _selectedFilm;
        public Film? SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }

        private Reservation? _selectedReservation;
        public Reservation? SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        private Seance? _selectedSeance;
        public Seance? SelectedSeance
        {
            get => _selectedSeance;
            set
            {
                _selectedSeance = value;
                OnPropertyChanged(nameof(SelectedSeance));
            }
        }

        private Ticket? _selectedTicket;
        public Ticket? SelectedTicket
        {
            get => _selectedTicket;
            set
            {
                _selectedTicket = value;
                OnPropertyChanged(nameof(SelectedTicket));
            }
        }

        public ObservableCollection<Actor> Actors { get; set; } = [];
        public ObservableCollection<Auditorium> Auditoriums { get; set; } = [];
        public ObservableCollection<Cinema> Cinemas { get; set; } = [];
        public ObservableCollection<Film> Films { get; set; } = [];
        public ObservableCollection<Reservation> Reservations { get; set; } = [];
        public ObservableCollection<Seance> Seances { get; set; } = [];
        public ObservableCollection<Ticket> Tickets { get; set; } = [];


        public ICommand CloseAppCommand { get; }
        public ICommand SeedSampleDataCommand { get; }
        public ICommand ClearDbCommand { get; }

        public ICommand AddActorCommand { get; }
        public ICommand EditActorCommand { get; }
        public ICommand DeleteActorCommand { get; }
        public ICommand ShowActorsCommand { get; }

        public ICommand AddAuditoriumCommand { get; }
        public ICommand EditAuditoriumCommand { get; }
        public ICommand DeleteAuditoriumCommand { get; }
        public ICommand ShowAuditoriumsCommand { get; }

        public ICommand AddCinemaCommand { get; }
        public ICommand EditCinemaCommand { get; }
        public ICommand DeleteCinemaCommand { get; }
        public ICommand ShowCinemasCommand { get; }

        public ICommand AddFilmCommand { get; }
        public ICommand EditFilmCommand { get; }
        public ICommand DeleteFilmCommand { get; }
        public ICommand ShowFilmsCommand { get; }

        public ICommand AddReservationCommand { get; }
        public ICommand EditReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }
        public ICommand ShowReservationsCommand { get; }

        public ICommand AddSeanceCommand { get; }
        public ICommand EditSeanceCommand { get; }
        public ICommand DeleteSeanceCommand { get; }
        public ICommand ShowSeancesCommand { get; }

        public ICommand AddTicketCommand { get; }
        public ICommand EditTicketCommand { get; }
        public ICommand DeleteTicketCommand { get; }
        public ICommand ShowTicketsCommand { get; }

        public MainViewModel()
        {
            try
            {
                _dbContext = DBManager.BuildDB();
                RefreshAllCollections();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error connecting to database: {ex.Message}");
            }

            CloseAppCommand = new Command(CloseApp);
            SeedSampleDataCommand = new Command(SeedSampleData);
            ClearDbCommand = new Command(ClearDb);

            AddActorCommand = new Command(AddActor);
            EditActorCommand = new Command(EditActor);
            DeleteActorCommand = new Command(DeleteActor);
            ShowActorsCommand = new Command(ShowActors);

            AddAuditoriumCommand = new Command(AddAuditorium);
            EditAuditoriumCommand = new Command(EditAuditorium);
            DeleteAuditoriumCommand = new Command(DeleteAuditorium);
            ShowAuditoriumsCommand = new Command(ShowAuditoriums);

            AddCinemaCommand = new Command(AddCinema);
            EditCinemaCommand = new Command(EditCinema);
            DeleteCinemaCommand = new Command(DeleteCinema);
            ShowCinemasCommand = new Command(ShowCinemas);

            AddFilmCommand = new Command(AddFilm);
            EditFilmCommand = new Command(EditFilm);
            DeleteFilmCommand = new Command(DeleteFilm);
            ShowFilmsCommand = new Command(ShowFilms);

            AddReservationCommand = new Command(AddReservation);
            EditReservationCommand = new Command(EditReservation);
            DeleteReservationCommand = new Command(DeleteReservation);
            ShowReservationsCommand = new Command(ShowReservations);

            AddSeanceCommand = new Command(AddSeance);
            EditSeanceCommand = new Command(EditSeance);
            DeleteSeanceCommand = new Command(DeleteSeance);
            ShowSeancesCommand = new Command(ShowSeances);

            AddTicketCommand = new Command(AddTicket);
            EditTicketCommand = new Command(EditTicket);
            DeleteTicketCommand = new Command(DeleteTicket);
            ShowTicketsCommand = new Command(ShowTickets);
        }

        private void RefreshAllCollections()
        {
            Actors = new ObservableCollection<Actor>(ActorService.GetAll(_dbContext));
            Auditoriums = new ObservableCollection<Auditorium>(AuditoriumService.GetAll(_dbContext));
            Cinemas = new ObservableCollection<Cinema>(CinemaService.GetAll(_dbContext));
            Films = new ObservableCollection<Film>(FilmService.GetAll(_dbContext));
            Reservations = new ObservableCollection<Reservation>(ReservationService.GetAll(_dbContext));
            Seances = new ObservableCollection<Seance>(SeanceService.GetAll(_dbContext));
            Tickets = new ObservableCollection<Ticket>(TicketService.GetAll(_dbContext));

            OnPropertyChanged(nameof(Actors));
            OnPropertyChanged(nameof(Auditoriums));
            OnPropertyChanged(nameof(Cinemas));
            OnPropertyChanged(nameof(Films));
            OnPropertyChanged(nameof(Reservations));
            OnPropertyChanged(nameof(Seances));
            OnPropertyChanged(nameof(Tickets));
        }

        private static void RefreshCollection<T>(ObservableCollection<T> collection, IEnumerable<T> newItems)
        {
            collection.Clear();
            foreach (var item in newItems)
            {
                collection.Add(item);
            }
        }

        private void CloseApp(object? parameter) => Application.Current.Shutdown();

        private void SeedSampleData(object? parameter)
        {
            try
            {
                ClearDb(null);

                AddTestData();

                RefreshAllCollections();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error seeding test data: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearDb(object? parameter)
        {
            try
            {
                DBManager.ClearDB();
                RefreshAllCollections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error dropping DB: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddActor(object? parameter)
        {
            IndexOfSelectedTab = 0;

            try
            {
                SelectedActor = new Actor("John", "Doe", "American", DateTime.Now.AddYears(-30),
                    "https://example.com/actor.jpg", "Actor biography...", 75.0);

                var window = new ActorWindow()
                {
                    DataContext = new ActorViewModel()
                    {
                        Actor = SelectedActor,
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newActor = ActorService.Add(_dbContext, SelectedActor.FirstName, SelectedActor.LastName,
                        SelectedActor.Nationality, SelectedActor.BirthDate, SelectedActor.ProfileImageUrl,
                        SelectedActor.Biography, SelectedActor.Popularity);

                    Actors.Add(newActor);
                    SelectedActor = newActor;

                    MessageBox.Show("New actor has been successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating actor: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditActor(object? parameter)
        {
            IndexOfSelectedTab = 0;
            if (SelectedActor is null)
            {
                MessageBox.Show("Please select actor to edit",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var actorCopy = new Actor(SelectedActor.FirstName, SelectedActor.LastName,
                    SelectedActor.Nationality, SelectedActor.BirthDate, SelectedActor.ProfileImageUrl,
                    SelectedActor.Biography, SelectedActor.Popularity);

                var window = new ActorWindow()
                {
                    DataContext = new ActorViewModel()
                    {
                        Actor = SelectedActor,
                    }
                };

                if (window.ShowDialog() == true)
                {
                    ActorService.Update(_dbContext, SelectedActor);

                    RefreshCollection(Actors, ActorService.GetAll(_dbContext));

                    MessageBox.Show("Actor has been successfully updated", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    SelectedActor.SetBiography(actorCopy.Biography);
                    SelectedActor.SetPopularity(actorCopy.Popularity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when editing actor: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteActor(object? parameter)
        {
            IndexOfSelectedTab = 0;
            if (SelectedActor is null)
            {
                MessageBox.Show("Select actor for deletion",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete this actor? {SelectedActor.FullName}? This action cannot be undone.",
                    "Deleting Actor", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ActorService.Delete(_dbContext, SelectedActor.Id);
                    Actors.Remove(SelectedActor);
                    SelectedActor = null;

                    MessageBox.Show("Actor successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting actor: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowActors(object? parameter)
        {
            IndexOfSelectedTab = 0;

            try
            {
                RefreshCollection(Actors, ActorService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading actors: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddAuditorium(object? parameter)
        {
            IndexOfSelectedTab = 1;

            try
            {
                if (!Cinemas.Any())
                {
                    MessageBox.Show("First create a cinema to add an auditorium.",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedAuditorium = new Auditorium(Cinemas.First().Id, "New Auditorium", 1, 10, 20);

                var window = new AuditoriumWindow()
                {
                    DataContext = new AuditoriumViewModel()
                    {
                        Auditorium = SelectedAuditorium,
                        AvailableCinemas = Cinemas
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newAuditorium = AuditoriumService.Add(_dbContext, SelectedAuditorium.CinemaId,
                        SelectedAuditorium.Name, SelectedAuditorium.RoomNumber,
                        SelectedAuditorium.Rows, SelectedAuditorium.SeatsPerRow);

                    Auditoriums.Add(newAuditorium);
                    SelectedAuditorium = newAuditorium;

                    MessageBox.Show("Auditorium successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding auditorium: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditAuditorium(object? parameter)
        {
            IndexOfSelectedTab = 1;
            if (SelectedAuditorium is null)
            {
                MessageBox.Show("Please select an auditorium to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new AuditoriumWindow()
                {
                    DataContext = new AuditoriumViewModel()
                    {
                        Auditorium = SelectedAuditorium,
                        AvailableCinemas = Cinemas
                    }
                };

                if (window.ShowDialog() == true)
                {
                    AuditoriumService.Update(_dbContext, SelectedAuditorium);
                    RefreshCollection(Auditoriums, AuditoriumService.GetAll(_dbContext));

                    MessageBox.Show("Auditorium successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing auditorium: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAuditorium(object? parameter)
        {
            IndexOfSelectedTab = 1;
            if (SelectedAuditorium is null)
            {
                MessageBox.Show("Please select an auditorium to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete auditorium {SelectedAuditorium.Name}? This action will also delete all seances in this auditorium.",
                    "Deleting Auditorium", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    AuditoriumService.Delete(_dbContext, SelectedAuditorium.Id);
                    Auditoriums.Remove(SelectedAuditorium);
                    SelectedAuditorium = null;

                    MessageBox.Show("Auditorium successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting auditorium: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowAuditoriums(object? parameter)
        {
            IndexOfSelectedTab = 1;
            try
            {
                RefreshCollection(Auditoriums, AuditoriumService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading auditoriums: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddCinema(object? parameter)
        {
            IndexOfSelectedTab = 2;

            try
            {
                SelectedCinema = new Cinema("New Cinema", "Address", "+380123456789",
                    "cinema@example.com", "Manager");

                var window = new CinemaWindow()
                {
                    DataContext = new CinemaViewModel()
                    {
                        Cinema = SelectedCinema,
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newCinema = CinemaService.Add(_dbContext, SelectedCinema.Name, SelectedCinema.Address,
                        SelectedCinema.ContactPhone, SelectedCinema.ContactEmail, SelectedCinema.ManagerName);

                    Cinemas.Add(newCinema);
                    SelectedCinema = newCinema;

                    MessageBox.Show("Cinema successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding cinema: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditCinema(object? parameter)
        {
            IndexOfSelectedTab = 2;
            if (SelectedCinema is null)
            {
                MessageBox.Show("Please select a cinema to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new CinemaWindow()
                {
                    DataContext = new CinemaViewModel()
                    {
                        Cinema = SelectedCinema,
                    }
                };

                if (window.ShowDialog() == true)
                {
                    CinemaService.Update(_dbContext, SelectedCinema);
                    RefreshCollection(Cinemas, CinemaService.GetAll(_dbContext));

                    MessageBox.Show("Cinema successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing cinema: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCinema(object? parameter)
        {
            IndexOfSelectedTab = 2;
            if (SelectedCinema is null)
            {
                MessageBox.Show("Please select a cinema to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete cinema {SelectedCinema.Name}? This action will also delete all auditoriums and seances in this cinema.",
                    "Deleting Cinema", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    CinemaService.Delete(_dbContext, SelectedCinema.Id);
                    Cinemas.Remove(SelectedCinema);
                    SelectedCinema = null;

                    MessageBox.Show("Cinema successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting cinema: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowCinemas(object? parameter)
        {
            IndexOfSelectedTab = 2;
            try
            {
                RefreshCollection(Cinemas, CinemaService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cinemas: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddFilm(object? parameter)
        {
            IndexOfSelectedTab = 3;

            try
            {
                SelectedFilm = new Film("New Film", "Film description", 120, "Director",
                    "Genre", false, "https://example.com/poster.jpg", "https://example.com/trailer.mp4");

                var window = new FilmWindow()
                {
                    DataContext = new FilmViewModel()
                    {
                        Film = SelectedFilm,
                        AvailableActors = Actors
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newFilm = FilmService.Add(_dbContext, SelectedFilm.Title, SelectedFilm.Description,
                        SelectedFilm.DurationMinutes, SelectedFilm.Director, SelectedFilm.Genre,
                        SelectedFilm.HasAgeRestriction, SelectedFilm.PosterUrl, SelectedFilm.TrailerUrl);

                    Films.Add(newFilm);
                    SelectedFilm = newFilm;

                    MessageBox.Show("Film successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding film: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditFilm(object? parameter)
        {
            IndexOfSelectedTab = 3;
            if (SelectedFilm is null)
            {
                MessageBox.Show("Please select a film to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new FilmWindow()
                {
                    DataContext = new FilmViewModel()
                    {
                        Film = SelectedFilm,
                        AvailableActors = Actors
                    }
                };

                if (window.ShowDialog() == true)
                {
                    FilmService.Update(_dbContext, SelectedFilm);
                    RefreshCollection(Films, FilmService.GetAll(_dbContext));

                    MessageBox.Show("Film successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing film: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteFilm(object? parameter)
        {
            IndexOfSelectedTab = 3;
            if (SelectedFilm is null)
            {
                MessageBox.Show("Please select a film to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete film {SelectedFilm.Title}? This action will also delete all seances of this film.",
                    "Deleting Film", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    FilmService.Delete(_dbContext, SelectedFilm.Id);
                    Films.Remove(SelectedFilm);
                    SelectedFilm = null;

                    MessageBox.Show("Film successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting film: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowFilms(object? parameter)
        {
            IndexOfSelectedTab = 3;
            try
            {
                RefreshCollection(Films, FilmService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading films: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddReservation(object? parameter)
        {
            IndexOfSelectedTab = 4;

            try
            {
                if (!Seances.Any())
                {
                    MessageBox.Show("First create a seance to add a reservation.",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedReservation = new Reservation(Seances.First().Id, "First Name", "Last Name",
                    "email@example.com", "+380123456789", "Cash");

                var window = new ReservationWindow()
                {
                    DataContext = new ReservationViewModel()
                    {
                        Reservation = SelectedReservation,
                        AvailableSeances = Seances
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newReservation = ReservationService.Add(_dbContext, SelectedReservation.SeanceId,
                        SelectedReservation.CustomerFirstName, SelectedReservation.CustomerLastName,
                        SelectedReservation.CustomerEmail, SelectedReservation.CustomerPhone,
                        SelectedReservation.PaymentMethod);

                    Reservations.Add(newReservation);
                    SelectedReservation = newReservation;

                    MessageBox.Show("Reservation successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding reservation: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditReservation(object? parameter)
        {
            IndexOfSelectedTab = 4;
            if (SelectedReservation is null)
            {
                MessageBox.Show("Please select a reservation to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new ReservationWindow()
                {
                    DataContext = new ReservationViewModel()
                    {
                        Reservation = SelectedReservation,
                        AvailableSeances = Seances
                    }
                };

                if (window.ShowDialog() == true)
                {
                    ReservationService.Update(_dbContext, SelectedReservation);
                    RefreshCollection(Reservations, ReservationService.GetAll(_dbContext));

                    MessageBox.Show("Reservation successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing reservation: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteReservation(object? parameter)
        {
            IndexOfSelectedTab = 4;
            if (SelectedReservation is null)
            {
                MessageBox.Show("Please select a reservation to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete reservation for {SelectedReservation.CustomerFullName}? This action will also delete all tickets of this reservation.",
                    "Deleting Reservation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ReservationService.Delete(_dbContext, SelectedReservation.Id);
                    Reservations.Remove(SelectedReservation);
                    SelectedReservation = null;

                    MessageBox.Show("Reservation successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting reservation: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowReservations(object? parameter)
        {
            IndexOfSelectedTab = 4;
            try
            {
                RefreshCollection(Reservations, ReservationService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddSeance(object? parameter)
        {
            IndexOfSelectedTab = 5;

            try
            {
                if (!Films.Any() || !Auditoriums.Any())
                {
                    MessageBox.Show("First create a film and an auditorium to add a seance.",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedSeance = new Seance(Films.First().Id, Auditoriums.First().Id,
                    DateTime.Now.AddDays(1), 150.0m, 120);

                var window = new SeanceWindow()
                {
                    DataContext = new SeanceViewModel()
                    {
                        Seance = SelectedSeance,
                        AvailableFilms = Films,
                        AvailableAuditoriums = Auditoriums
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newSeance = SeanceService.Add(_dbContext, SelectedSeance.FilmId, SelectedSeance.AuditoriumId,
                        SelectedSeance.StartTime, SelectedSeance.Price, SelectedSeance.FilmDurationMinutes);

                    Seances.Add(newSeance);
                    SelectedSeance = newSeance;

                    MessageBox.Show("Seance successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding seance: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditSeance(object? parameter)
        {
            IndexOfSelectedTab = 5;
            if (SelectedSeance is null)
            {
                MessageBox.Show("Please select a seance to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new SeanceWindow()
                {
                    DataContext = new SeanceViewModel()
                    {
                        Seance = SelectedSeance,
                        AvailableFilms = Films,
                        AvailableAuditoriums = Auditoriums
                    }
                };

                if (window.ShowDialog() == true)
                {
                    SeanceService.Update(_dbContext, SelectedSeance);
                    RefreshCollection(Seances, SeanceService.GetAll(_dbContext));

                    MessageBox.Show("Seance successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing seance: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSeance(object? parameter)
        {
            IndexOfSelectedTab = 5;
            if (SelectedSeance is null)
            {
                MessageBox.Show("Please select a seance to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete seance {SelectedSeance.StartTime}? This action will also delete all reservations and tickets of this seance.",
                    "Deleting Seance", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    SeanceService.Delete(_dbContext, SelectedSeance.Id);
                    Seances.Remove(SelectedSeance);
                    SelectedSeance = null;

                    MessageBox.Show("Seance successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting seance: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowSeances(object? parameter)
        {
            IndexOfSelectedTab = 5;
            try
            {
                RefreshCollection(Seances, SeanceService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading seances: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTicket(object? parameter)
        {
            IndexOfSelectedTab = 6;

            try
            {
                if (!Reservations.Any() || !Cinemas.Any() || !Auditoriums.Any() || !Seances.Any() || !Films.Any())
                {
                    MessageBox.Show("First create all necessary entities for a ticket (reservation, cinema, auditorium, seance, film).",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                SelectedTicket = new Ticket(Reservations.First().Id, Cinemas.First().Id, Auditoriums.First().Id,
                    Seances.First().Id, Films.First().Id, "A1", 150.0m, TicketType.Standard);

                var window = new TicketWindow()
                {
                    DataContext = new TicketViewModel()
                    {
                        Ticket = SelectedTicket,
                        AvailableReservations = Reservations,
                        AvailableCinemas = Cinemas,
                        AvailableAuditoriums = Auditoriums,
                        AvailableSeances = Seances,
                        AvailableFilms = Films
                    }
                };

                if (window.ShowDialog() == true)
                {
                    var newTicket = TicketService.Add(_dbContext, SelectedTicket.ReservationId, SelectedTicket.CinemaId,
                        SelectedTicket.AuditoriumId, SelectedTicket.SeanceId, SelectedTicket.FilmId,
                        SelectedTicket.SeatId, SelectedTicket.OriginalPrice, SelectedTicket.Type);

                    Tickets.Add(newTicket);
                    SelectedTicket = newTicket;

                    MessageBox.Show("Ticket successfully added!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding ticket: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditTicket(object? parameter)
        {
            IndexOfSelectedTab = 6;
            if (SelectedTicket is null)
            {
                MessageBox.Show("Please select a ticket to edit.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var window = new TicketWindow()
                {
                    DataContext = new TicketViewModel()
                    {
                        Ticket = SelectedTicket,
                        AvailableReservations = Reservations,
                        AvailableCinemas = Cinemas,
                        AvailableAuditoriums = Auditoriums,
                        AvailableSeances = Seances,
                        AvailableFilms = Films
                    }
                };

                if (window.ShowDialog() == true)
                {
                    TicketService.Update(_dbContext, SelectedTicket);
                    RefreshCollection(Tickets, TicketService.GetAll(_dbContext));

                    MessageBox.Show("Ticket successfully updated!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing ticket: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTicket(object? parameter)
        {
            IndexOfSelectedTab = 6;

            if (SelectedTicket is null)
            {
                MessageBox.Show("Please select a ticket to delete.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Delete ticket for seat {SelectedTicket.SeatId}?",
                    "Deleting Ticket", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    TicketService.Delete(_dbContext, SelectedTicket.Id);
                    Tickets.Remove(SelectedTicket);
                    SelectedTicket = null;

                    MessageBox.Show("Ticket successfully deleted!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting ticket: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowTickets(object? parameter)
        {
            IndexOfSelectedTab = 6;

            try
            {
                RefreshCollection(Tickets, TicketService.GetAll(_dbContext));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tickets: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTestData()
        {
            try
            {
                // Actors
                var actor1 = new Actor("Tom", "Hanks", "American", new DateTime(1956, 7, 9),
                    "https://example.com/tom_hanks.jpg", "Famous American actor known for Forrest Gump and Cast Away.", 95.5);
                var actor2 = new Actor("Meryl", "Streep", "American", new DateTime(1949, 6, 22),
                    "https://example.com/meryl_streep.jpg", "Legendary American actress with multiple Academy Awards.", 98.2);
                var actor3 = new Actor("Leonardo", "DiCaprio", "American", new DateTime(1974, 11, 11),
                    "https://example.com/leo.jpg", "Academy Award winner known for Titanic and The Revenant.", 97.8);
                var actor4 = new Actor("Scarlett", "Johansson", "American", new DateTime(1984, 11, 22),
                    "https://example.com/scarlett.jpg", "Highest-grossing actress known for Marvel films.", 96.3);
                var actor5 = new Actor("Denzel", "Washington", "American", new DateTime(1954, 12, 28),
                    "https://example.com/denzel.jpg", "Two-time Academy Award winner and renowned dramatic actor.", 94.7);
                var actor6 = new Actor("Cate", "Blanchett", "Australian", new DateTime(1969, 5, 14),
                    "https://example.com/cate.jpg", "Two-time Academy Award winner known for versatile roles.", 93.2);

                // Cinemas
                var cinema1 = new Cinema("Multiplex Cinema", "123 Main Street, Kyiv", "+380441234567", "info@multiplex.ua", "Ivan Petrenko");
                var cinema2 = new Cinema("Star Cinema", "456 Central Avenue, Kyiv", "+380441234568", "info@starcinema.ua", "Olena Kovalenko");
                var cinema3 = new Cinema("City Lights", "789 Broadway, Kyiv", "+380441234569", "info@citylights.ua", "Petro Sydorenko");

                // Auditoriums
                var auditorium1 = new Auditorium(cinema1.Id, "IMAX Hall", 1, 12, 20);
                auditorium1.AddItem("Dolby Atmos");
                auditorium1.AddItem("3D Projection");
                auditorium1.AddItem("Laser Projection");

                var auditorium2 = new Auditorium(cinema1.Id, "VIP Hall", 2, 8, 15);
                auditorium2.AddItem("Recliner Seats");
                auditorium2.AddItem("Food Service");
                auditorium2.AddItem("Dolby 7.1");

                var auditorium3 = new Auditorium(cinema2.Id, "Main Hall", 1, 10, 18);
                auditorium3.AddItem("Dolby Digital");
                auditorium3.AddItem("3D Ready");

                var auditorium4 = new Auditorium(cinema3.Id, "Premium Hall", 1, 6, 12);
                auditorium4.AddItem("4K Projection");
                auditorium4.AddItem("Atmos Sound");
                auditorium4.AddItem("Butler Service");

                // Auditorium Ratings
                auditorium1.AddRating(5);
                auditorium1.AddRating(4);
                auditorium2.AddRating(5);
                auditorium2.AddRating(5);
                auditorium3.AddRating(4);
                auditorium3.AddRating(3);

                // Films
                var film1 = new Film("Forrest Gump", "The story of a man with low IQ who accomplished great things in his life",
                    142, "Robert Zemeckis", "Drama", false,
                    "https://example.com/forrest_gump.jpg", "https://example.com/forrest_trailer");

                var film2 = new Film("Inception", "A thief who steals corporate secrets through dream-sharing technology",
                    148, "Christopher Nolan", "Sci-Fi", false,
                    "https://example.com/inception.jpg", "https://example.com/inception_trailer");

                var film3 = new Film("The Dark Knight", "Batman faces the Joker, a criminal mastermind seeking to create chaos",
                    152, "Christopher Nolan", "Action", true,
                    "https://example.com/dark_knight.jpg", "https://example.com/dark_knight_trailer");

                var film4 = new Film("The Shawshank Redemption", "Two imprisoned men bond over a number of years finding solace",
                    142, "Frank Darabont", "Drama", false,
                    "https://example.com/shawshank.jpg", "https://example.com/shawshank_trailer");

                var film5 = new Film("Avengers: Endgame", "The Avengers take one final stand against Thanos",
                    181, "Anthony Russo", "Action", false,
                    "https://example.com/endgame.jpg", "https://example.com/endgame_trailer");

                var film6 = new Film("La La Land", "A jazz pianist and an aspiring actress pursue their dreams in Los Angeles",
                    128, "Damien Chazelle", "Musical", false,
                    "https://example.com/lalaland.jpg", "https://example.com/lalaland_trailer");

                // Adding Actors To Films
                film1.AddItem(actor1.Id);
                film2.AddItem(actor3.Id);
                film2.AddItem(actor4.Id);
                film3.AddItem(actor3.Id);
                film3.AddItem(actor5.Id);
                film4.AddItem(actor1.Id);
                film5.AddItem(actor4.Id);
                film6.AddItem(actor3.Id);
                film6.AddItem(actor6.Id);

                // Adding Films To Cinemas
                cinema1.AddItem(film1.Id);
                cinema1.AddItem(film2.Id);
                cinema1.AddItem(film3.Id);
                cinema1.AddItem(film4.Id);

                cinema2.AddItem(film2.Id);
                cinema2.AddItem(film3.Id);
                cinema2.AddItem(film5.Id);

                cinema3.AddItem(film1.Id);
                cinema3.AddItem(film4.Id);
                cinema3.AddItem(film6.Id);

                // Film Ratings
                film1.AddRating(5);
                film1.AddRating(4);
                film1.AddRating(5);
                film2.AddRating(5);
                film2.AddRating(5);
                film2.AddRating(4);
                film3.AddRating(5);
                film3.AddRating(5);
                film3.AddRating(5);
                film4.AddRating(5);
                film4.AddRating(5);
                film4.AddRating(4);
                film5.AddRating(4);
                film5.AddRating(4);
                film5.AddRating(3);
                film6.AddRating(4);
                film6.AddRating(5);

                // Cinema Ratings
                cinema1.AddRating(5);
                cinema1.AddRating(4);
                cinema1.AddRating(5);
                cinema2.AddRating(4);
                cinema2.AddRating(4);
                cinema2.AddRating(3);
                cinema3.AddRating(5);
                cinema3.AddRating(5);

                // Seances
                var seance1 = new Seance(film1.Id, auditorium1.Id, DateTime.Now.AddSeconds(4), 250.0m, film1.DurationMinutes);
                var seance2 = new Seance(film2.Id, auditorium1.Id, DateTime.Now.AddHours(5), 280.0m, film2.DurationMinutes);
                var seance3 = new Seance(film3.Id, auditorium2.Id, DateTime.Now.AddHours(6), 350.0m, film3.DurationMinutes);
                var seance4 = new Seance(film4.Id, auditorium3.Id, DateTime.Now.AddHours(8), 200.0m, film4.DurationMinutes);
                var seance5 = new Seance(film5.Id, auditorium3.Id, DateTime.Now.AddHours(10), 300.0m, film5.DurationMinutes);
                var seance6 = new Seance(film6.Id, auditorium4.Id, DateTime.Now.AddHours(25), 400.0m, film6.DurationMinutes);

                // Adding Reserved Seats
                seance1.ReserveSeat("A1", auditorium1.Capacity);
                seance1.ReserveSeat("A2", auditorium1.Capacity);
                seance1.ReserveSeat("B5", auditorium1.Capacity);

                seance3.ReserveSeat("C3", auditorium2.Capacity);
                seance3.ReserveSeat("C4", auditorium2.Capacity);
                seance3.ReserveSeat("D1", auditorium2.Capacity);
                seance3.ReserveSeat("D2", auditorium2.Capacity);

                seance6.ReserveSeat("A1", auditorium4.Capacity);
                seance6.ReserveSeat("A2", auditorium4.Capacity);

                // Reservations
                var reservation1 = new Reservation(seance1.Id, "John", "Doe", "john.doe@email.com", "+380501234567", "Credit Card");
                var reservation2 = new Reservation(seance3.Id, "Jane", "Smith", "jane.smith@email.com", "+380502345678", "Cash");
                var reservation3 = new Reservation(seance6.Id, "Bob", "Johnson", "bob.johnson@email.com", "+380503456789", "Online Payment");

                // Tickets
                var ticket1 = new Ticket(reservation1.Id, cinema1.Id, auditorium1.Id, seance1.Id, film1.Id, "A3", seance1.Price, TicketType.Standard);
                var ticket2 = new Ticket(reservation1.Id, cinema1.Id, auditorium1.Id, seance1.Id, film1.Id, "A4", seance1.Price, TicketType.Student);
                var ticket3 = new Ticket(reservation2.Id, cinema1.Id, auditorium2.Id, seance3.Id, film3.Id, "C5", seance3.Price, TicketType.VIP);
                var ticket4 = new Ticket(reservation3.Id, cinema3.Id, auditorium4.Id, seance6.Id, film6.Id, "A3", seance6.Price, TicketType.VIP);
                var ticket5 = new Ticket(reservation3.Id, cinema3.Id, auditorium4.Id, seance6.Id, film6.Id, "A4", seance6.Price, TicketType.Standard);


                // Saving To DB
                _dbContext.Actors.AddRange([actor1, actor2, actor3, actor4, actor5, actor6]);
                _dbContext.Cinemas.AddRange([cinema1, cinema2, cinema3]);
                _dbContext.Auditoriums.AddRange([auditorium1, auditorium2, auditorium3, auditorium4]);
                _dbContext.Films.AddRange([film1, film2, film3, film4, film5, film6]);
                _dbContext.Seances.AddRange([seance1, seance2, seance3, seance4, seance5, seance6]);
                _dbContext.Reservations.AddRange([reservation1, reservation2, reservation3]);
                _dbContext.Tickets.AddRange([ticket1, ticket2, ticket3, ticket4, ticket5]);

                _dbContext.SaveChanges();

                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating test data: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}