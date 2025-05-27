using UnityEngine;

public class Peca : MonoBehaviour
{
    private static Peca pecaSelecionada;
    
    private void OnMouseDown()
    {
        if (pecaSelecionada == null)
        {
            pecaSelecionada = this;
        }
        else
        {
            // Troca as posições
            Vector3 temp = pecaSelecionada.transform.position;
            pecaSelecionada.transform.position = transform.position;
            transform.position = temp;

            // Adiciona no histórico para desfazer/replay
            PuzzleManager.Instance.RegistrarComando(pecaSelecionada, this);
            PuzzleManager.Instance.SeraseTerminou();
            
            pecaSelecionada = null;
            
            
        }
        
    }
}
