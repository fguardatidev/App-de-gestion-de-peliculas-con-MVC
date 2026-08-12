Status: Work in progress.

Proyecto basado inicialmente en el tutorial ASP.NET Core MVC de Microsoft y posteriormente extendido con fines de aprendizaje, incorporando prácticas de arquitectura y desarrollo utilizadas en entornos profesionales.

Mejoras implementadas:
* Implementación de una arquitectura Controller → Service → Repository, separando responsabilidades y buscando reducir el acoplamiento entre las distintas capas de la aplicación.
* Incorporación de la entidad Genre para gestionar los géneros de las películas de manera independiente. Se estableció una relación entre Movie y Genre, almacenando la clave foránea GenreId en Movie.
