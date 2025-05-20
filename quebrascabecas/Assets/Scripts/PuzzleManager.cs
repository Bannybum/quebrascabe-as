using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    
    public List<Transform> pecas;
    private Vector3[] posicoesIniciais;
   
    public static PuzzleManager Instance;
    private Stack<ICommand> historico = new Stack<ICommand>();
    private List<ICommand> comandosReplay = new List<ICommand>();

    void Awake()
    {
        Instance = this;
    }
    

    void Start()
    {
        posicoesIniciais = new Vector3[pecas.Count];
        for (int i = 0; i < pecas.Count; i++)
        {
            posicoesIniciais[i] = pecas[i].position;
        }

        Embaralhar();
    }
    
    public void Embaralhar()
    {
        for (int i = 0; i < pecas.Count; i++)
        {
            int j = Random.Range(0, pecas.Count);
            Vector3 temp = pecas[i].position;
            pecas[i].position = pecas[j].position;
            pecas[j].position = temp;
        }
    }
    
    public void RegistrarComando(Peca a, Peca b)
    {
        ICommand comando = new CommandTroca(a, b);
        historico.Push(comando);
        comandosReplay.Add(comando);
    }
    
    
    void SeraseTerminou()
    {
        for (int i = 0; i < pecas.Count; i++)
        {
            if (pecas[i].position != posicoesIniciais[i])
                return;
        }
        
        Debug.Log("Ae meno :D");
    }

    public void DesfazerUltimo()
    {
        if (historico.Count > 0)
        {
            var comando = historico.Pop();
            comando.Undo();
        }
    }

    
    public void JogarNovamente()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Puzzle");
    }
    
    
    public void IniciarReplay()
    {
        StartCoroutine(ExecutarReplay());
    }

    private IEnumerator<WaitForSeconds> ExecutarReplay()
    {
        // Reseta
        UnityEngine.SceneManagement.SceneManager.LoadScene("Puzzle");

        yield return new WaitForSeconds(1f);

        foreach (var comando in comandosReplay)
        {
            comando.Do();
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Replay :D");
    }
    
    public void CancelarReplay()
    {
        StopAllCoroutines();

        foreach (var comando in comandosReplay)
        {
            comando.Do();
        }

        Debug.Log("Paro");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
