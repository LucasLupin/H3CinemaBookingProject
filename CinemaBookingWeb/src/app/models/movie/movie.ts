import { Genre } from "../genre/genre"

export class Movie
{
    movieID?: number
    title?: string
    duration?: number
    director?: string
    movieLink?: string
    genres?: Genre[];
}
