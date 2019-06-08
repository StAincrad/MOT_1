//Identifica al jugador
public enum Player {left, right}

//Estado del juego
public enum State
{
    start,  //Principio del juego, antes del primer servicio
    serve,  //Esperando una pulsación de tecla para poner el la bola en juego
    play,   //La bola está en juego, rebotando entre las palas
    done    //juego terminado, con un vencedor, listo para reempezar
}