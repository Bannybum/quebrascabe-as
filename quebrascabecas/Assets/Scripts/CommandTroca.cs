using UnityEngine;

public class CommandTroca : ICommand
{
    private Peca a, b;

    public CommandTroca(Peca a, Peca b)
    {
        this.a = a;
        this.b = b;
    }

    public void Do()
    {
        Trocar();
    }

    public void Undo()
    {
        Trocar();
    }

    private void Trocar()
    {
        Vector3 temp = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = temp;
    }
}

