import { Injectable, inject } from '@angular/core'; // 'inject' es la nueva moda
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

// Definimos la interfaz aquí mismo o en un archivo aparte
export interface Reserva {
  id: number;
  nombre: string;
  email: string;
  fechaHora: string;
  cantidadPersonas: number;
  estado: string;
  tipo: string;
  codigoVip?: string;
  mesaPreferida?: string;
  edadCumpleanero?: number;
  requiereTorta?: boolean;
}

export interface PagedResult<T> {
  items: T[];
  totalRegistros: number;
}

@Injectable({
  providedIn: 'root'
})
export class ReservaService {
  private apiUrl = 'http://localhost:5267/api/reservas'; 
  
  private http = inject(HttpClient);

  getReservas(pagina: number = 1): Observable<PagedResult<Reserva>> {
    const params = new HttpParams()
      .set('pagina', pagina)
      .set('registrosPorPagina', 10);

    return this.http.get<PagedResult<Reserva>>(this.apiUrl, { params });
  }

  getReserva(id: number): Observable<Reserva> {
  // GET http://localhost:5267/api/reservas/5
  return this.http.get<Reserva>(`${this.apiUrl}/${id}`);
}
}