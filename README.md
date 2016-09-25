<img src="WindowsFormsApplication1/Images/Bug-LineMaxSize.png" align="center" alt="Footnote"/>

#TCCInterface

Este projeto compõe um Trabalho de Conclusão de Curso do curso de Ciência da Computação da Universidade Estadual Paulista (UNESP) 
Campus Bauru. O trabalho em questão é da área de segurança de redes de computadores, com foco no processo de detecção de anomalias.

O principal objetivo deste trabalho foi propor uma abordagem baseada na classificação e otimização das características dos pacotes 
capturados na rede. A etapa de classificação é responsável por aprender o que é anomalia, e assim, ter a capacidade de classificar 
novas amostras em duas possíveis classes: anomalia ou não anomalia, e a outra etapa é responsável pela otimização da função que 
representa tais características, com o intuito de encontrar o melhor valor para aumentar a acurácia na detecção.

## Classificação
O classificador utilizado foi baseado em Florestas de Caminhos Ótimos (*Optimum-Path Forest* - OPF), o qual utiliza uma metodologia 
baseada em grafos e seu funcionamento parte do princípio da seleção de protótipos (amostras mais representativas das classes) os quais 
devem competir entre si para conquistar os nós adjacentes, definindo, então, suas classes. <cite>[1]</cite>

## Otimização

A otimização utilizada foi a Otimização por Enxame de Partículas (*Particle Swarm Optimization* - PSO), a qual é baseada no 
comportamento social biológico, especificamente no comportamento social de bandos de pássaros e cardumes de peixes, os quais têm 
capacidade de aprender e transmitir conhecimento para outros indivíduos da população. Este compartilhamento de conhecimento influencia 
as decisões dos indivíduos, que buscam seguir a melhor opção. <cite>[2]</cite>

## Interface Gráfica





## Referências Bibliográficas

[1] PAPA, J. P.; FALCãO, A. X.; SUZUKI, C. T. N. Supervised pattern classification based on optimum-path forest. International Journal 
of Imaging Systems and Technology, Wiley Subscription Services, Inc., A Wiley Company, v. 19, n. 2, p. 120–131, 2009. ISSN 1098-1098.

KENNEDY, J.; EBERHART, R. Particle swarm optimization. In: Neural Networks, 1995. Proceedings., IEEE International Conference on. 
[S.l.: s.n.], 1995. v. 4, p. 1942–1948 vol.4.
