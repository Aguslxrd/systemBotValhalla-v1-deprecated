# systemBotValhalla

<h1>DOCUMENTACION ValhallaBOT</h1>
<p>
<h5>
-(Encuesta)
!poll TiempoLimite(en seg) opcion1 opcion2 opcion3 opcion4 titulo de la encuesta
</h5>
<h5>
-(IP)
!ip - > Devuelve un mensaje "Embed" con la ip del servidor</h5>
<h5>
-(redes)
!redes - > Devuelve un mensaje "Embed" con las redes del servidor
</h5>
<h5>
-(Status) (Funciona solo en el discord SoftUP por seguridad)
!status - > Devuelve un mensaje "Embed" con las estadisticas del servidor
</h5>
<h5>
-(alerta)
!alerta <Mensaje>  - - > Envia un msg alerta al canal actual.
</h5>
(ayuda)
!ayuda - > Despliega un menu con botones y funciones de ayuda del bot.
</h5>
</p>
<h5>
-
</h5>

<h5>
-!setactivity - > Establece el "Yggdrasil" viendo como estado (hardcodeado)
</h5>

<h5>
-!mute - > !mute <nick> <duracionEnSegundos>
</h5>

<h5>
-!kick - > !kick <nick> <razon>
</h5>

<h5>
-!ban - > !ban <nick> <razon>
</h5>

<h5>
-!ping | !latencia - > Genera un ping del cliente hacia el servidor y lo devuelve calculando la latencia/ms
</h5>

<h5>
-
</h5>
<br> </br>
<h1>Guia de instalacion</h1>
<p>
Para ejecutarlo en ubuntu/linux se necesita "mono"
sudo apt install mono-complete
(1) xbuild nombreSolucion.sln  -  - >  Compila con "xbuild"
(2) mono nombreBot.exe -  - > Ejecuta el .exe con "mono"
para ejecutarlo, el archivo config.json debe estar en la misma carpeta que el .exe, de lo contrario el bot no encontrara el token.
</p>
