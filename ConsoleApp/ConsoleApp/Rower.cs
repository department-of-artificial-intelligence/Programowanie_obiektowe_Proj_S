namespace ConsoleApp;

public class Rower : IZmienStatus {
    private int id;
    private string model;
    private string status;

    public Rower(int id, string model, string status) {
        this.id = id;
        this.model = model;
        this.status = status;
    }

    public void ZmienStatus(string nowyStatus) {
        status = nowyStatus;
    }

    public override string ToString() => $"ID: {id} \nROWER: {model} \nSTATUS: {status}";
}