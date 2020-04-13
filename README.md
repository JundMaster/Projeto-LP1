# Wolf and Sheep

### Grupo 08: 

Gonçalo Verde (a21901395)\
Pedro Marques (a21900253)\
Luiz Santos (a21901441)

---
### Tarefas realizadas no exercício
Tarefas

---
### Repositório git
https://github.com/JundMaster/Projeto-LP1.git

---
### Descrição da solução
Para a resolução deste exercício começámos por criar uma classe _Square_ para cada "quadrado" do tabuleiro, de modo a termos acesso a determinadas características dos mesmos, como a _row_, _column_ e _isPlayable_ para saber se está ocupado. A nossa lógica foi de que o _Square_ - ou não pode ser jogado, ou pode ser jogado, ou está ocupado por um animal -. Após a criação da classe, construímos uma função com um array bidemensional  para criar um tabuleiro 8x8, onde todo o jogo vai decorrer. Este tabuleiro vai ser desenhado num _for loop_, em que vão ser desenhados todos os quadrados do tabuleiro juntamente com os animais.

Após isto optámos por criar os _loops_ de _gameplay_, nos quais começámos por criar o _input_ do utilizador para a movimentação tanto do lobo como das ovelhas, no qual utilizamos os  números da _column_ e da _row_ (da classe _Square_) para mudar a posição dos mesmos. Para a lógica da movimentação de um quadrado por turno, criámos algum código que só aceita números à volta da posição do animal, que rejeita o _input_ se o quadrado estiver a mais de duas  unidades de distância. As jogadas são controladas através de números par/ímpar, que vão definir se é o turno do lobo ou das ovelhas. Durante esta fase criámos também alguns métodos simples, como o _intro()_ e o _victory()_, que servem apenas para introdução do jogo e a vitória no final.

---
### Arquitetura do código
#### Métodos:
- #### _main()_;
  - A função apenas corre a _intro()_ e a _game()_;

- #### _game()_;
  - Responsável pela _loop_ inteiro do jogo. 
  - Contém todas as variáveis necessárias para o programa. 
  - Recebe inputs e imprime o tabuleiro de jogo;

- #### _intro()_;
  - Imprime regras do jogo no ínicio;

- #### _victory()_;
  - Imprime mensagem de vitória no final do jogo;

#### Classes:
- #### _Square_;
  - Contém a _row_, _column_ e _isPlayable;

---
### Fluxograma

---
### Trocas de ideias/referências

#### Criação da classe _Square_
- Sluiter, Shad."C# Chess Board 02 board cell classes" _Youtube_, uploaded by shad sluiter, Jun 13, 2019, youtu.be/SFMVyiJ2S6g
