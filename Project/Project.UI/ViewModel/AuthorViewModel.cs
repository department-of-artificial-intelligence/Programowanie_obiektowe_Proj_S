using Project.DTO;
using Project.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Project.UI.ViewModel;

public class AuthorViewModel : INotifyPropertyChanged
{
    private readonly IAuthorService _authorService;
    private readonly AuthorDto? _existingAuthor;
    
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private DateTime _birthDay = DateTime.Now.AddYears(-30);

    public bool IsEditMode => _existingAuthor != null;

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime BirthDay
    {
        get => _birthDay;
        set
        {
            if (_birthDay != value)
            {
                _birthDay = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event EventHandler<bool>? RequestClose;
    public event PropertyChangedEventHandler? PropertyChanged;

    public AuthorViewModel(IAuthorService authorService, AuthorDto? existingAuthor = null)
    {
        _authorService = authorService;
        _existingAuthor = existingAuthor;

        SaveCommand = new AsyncRelayCommand(async _ => await SaveAsync());
        CancelCommand = new RelayCommand(_ => Cancel());

        if (_existingAuthor != null)
        {
            FirstName = _existingAuthor.FirstName;
            LastName = _existingAuthor.LastName;
            BirthDay = _existingAuthor.BirthDay;
        }
    }

    private async Task SaveAsync()
    {
        try
        {
            var authorDto = new AuthorDto
            {
                Id = _existingAuthor?.Id ?? 0,
                FirstName = FirstName,
                LastName = LastName,
                BirthDay = BirthDay
            };

            if (IsEditMode)
            {
                await _authorService.UpdateAsync(_existingAuthor!.Id, authorDto);
            }
            else
            {
                await _authorService.CreateAsync(authorDto);
            }
            
            RequestClose?.Invoke(this, true);
        }
        catch (Exception ex)
        {
            
        }
    }

    private void Cancel()
    {
        RequestClose?.Invoke(this, false);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
