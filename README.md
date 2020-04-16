# Wolf and Sheep

### Grupo 08: 

Gonçalo Verde (a21901395)\
Pedro Marques (a21900253)\
Luiz Santos (a21901441)

---
### Tarefas realizadas no exercício
A maior parte das tarefas foram realizadas por mais de um elemento.

Tarefas realizadas por ordem cronológica
|Gonçalo Verde|Luiz Santos|Pedro Marques|
|---	|---	|---	|
|Input inicial do Utilizador|Classe Square|Classe Square|
|Criação das ovelhas|_Gameloop_ geral|Criação do Lobo/Método _intro_ /Método _victory_|
|_Gameloop_ das ovelhas|Movimentação com input|_Gameloop_ do lobo|
|Limitação do movimento das ovelhas|Try-Catch do lobo|Limitação do movimento do lobo|
|Método _wolfFreePlays_ / Método _wolfGameOver_|Try-Catch das ovelhas|Jogadas possíveis do lobo / Método _printBoard_|
|Método _sheepGameOver_ / Método sheepChosen|Método _checkConvert_|Método _legalMove_|
|Documentação XML|Fluxograma|Relatório|

---
### Repositório git
https://github.com/JundMaster/Projeto-LP1.git

---
### Descrição da solução
Para a resolução deste exercício começámos por criar uma classe _Square_ para cada "quadrado" do tabuleiro, de modo a termos acesso a determinadas características dos mesmos, como a _row_, _column_ e _isPlayable_ para saber se está ocupado. A lógica foi  que o _Square_ - ou não pode ser jogado, ou pode ser jogado, ou está ocupado por um animal -. Após a criação da classe, construímos uma função com um array bidemensional  para criar um tabuleiro 8x8, onde todo o jogo vai decorrer. Este tabuleiro vai ser desenhado com um _for loop_, numa função à parte, onde vão ser apresentados todos os quadrados do tabuleiro juntamente com os animais.

Após isto optámos por criar os _loops_ de _gameplay_ com _do whiles_, nos quais começámos por criar o _input_ do utilizador para a movimentação tanto do lobo como das ovelhas, em que utilizámos os números da _column_ e da _row_ (da classe _Square_) para mudar a posição dos mesmos. De modo a controlar os movimentos dos animais, utilizamos condições que são aceites apenas quando a jogada é possível (_isPlayable==true_), tendo sido esta condição na qual nos baseámos para a maior parte do código. As jogadas são controladas através de números par/ímpar, que vão definir se é o turno do lobo ou das ovelhas. O código para o lobo e para as ovelhas é semelhante em todos os aspetos. 

Entretanto, de modo a limitar _inputs_ não desejados do utilizador, criámos um _try-catch_ que imprime mensagens de erro caso a opção introduzida não seja uma possibilidade. Posteriormente foi criado o método _checkConvert_ para este efeito.

Mais tarde, para uma melhor organização do código, dividimos as "responsabilidades" em métodos que executam a movimentação dos animais, a confirmação de jogadas possíveis e condições que terminam o jogo.

---
### Arquitetura do código
#### Métodos:
- #### _main()_;
  - O método corre a _intro()_ e a _game()_;

- #### _game()_;
  - Responsável pela _loop_ inteiro do jogo. 

- #### _intro()_;
  - Imprime regras do jogo no ínicio;

- #### _victory()_;
  - Imprime mensagem de vitória no final do jogo;
  
- #### _legalMove()_;
  - Só aceita números com +1/-1 que a casa atual;
  
- #### _wolfFreePlays()_;
  - Imprime jogadas possíveis do lobo;

- #### _wolfGameOver()_;
  - Verifica se o lobo ainda tem jogadas possíveis, caso não tenha o jogo é terminado;
  
- #### _sheepGameOver()_;
  - Verifica se a ovelha ainda tem jogadas possíveis, caso não tenha o jogo é terminado;
 
- #### _sheepChosen()_;
  - Verifica qual a ovelha que o jogador escolheu;
  
- #### _printBoard()_;
  - Imprime o tabuleiro do jogo;

- #### _checkConvert()_;
  - Imprime mensagens de erros se o _input_ do utilizar não estiver correto;
  
- #### _firstTurn()_;
  - Primeiro turno do jogo;

- #### _wolfTurn()_;
  - Turnos do lobo;

- #### _sheepTurn()_;
  - Turnos da ovelha;
  
#### Classes:
- #### _Square_;
  - Contém a _row_, _column_ e _isPlayable_;

---
### Fluxograma

---
### Trocas de ideias/referências

#### Criação da classe _Square_
- Sluiter, Shad."C# Chess Board 02 board cell classes" _Youtube_, uploaded by shad sluiter, Jun 13, 2019, https://www.youtube.com/watch?v=SFMVyiJ2S6g&feature=youtu.be
#### Try-Catch
- "C# documentation", _Microsoft_, Microsoft 2020,
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch
