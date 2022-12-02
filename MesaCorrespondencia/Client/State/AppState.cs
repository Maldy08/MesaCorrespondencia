namespace MesaCorrespondencia.Client.State
{
    public class AppState
    {
        private int ejercicio;
        public event Action OnChange;
        

        public int Ejercicio
        {
            get { return ejercicio; }
            set
            {
                if (ejercicio != value)
                {
                    ejercicio = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
  
}
