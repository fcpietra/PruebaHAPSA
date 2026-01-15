import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router'; 
import { Reserva, ReservaService } from '../../services/reserva.service';

@Component({
  selector: 'app-detalle-reserva',
  standalone: true,
  imports: [CommonModule, DatePipe, RouterLink], 
  templateUrl: './detalle-reserva.component.html',
  styleUrl: './detalle-reserva.component.css'
})
export class DetalleReservaComponent implements OnInit {
  
  private route = inject(ActivatedRoute); // Para acceder a la URL (ej: /detalle/5)
  private reservaService = inject(ReservaService);

  reserva?: Reserva; 
  cargando = true;

  ngOnInit(): void {
    // 1. Obtenemos el ID de la URL
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      // 2. Llamamos al servicio
      this.reservaService.getReserva(Number(id)).subscribe({
        next: (data : Reserva) => {
          this.reserva = data;
          this.cargando = false;
        },
        error: (err : any) => {
          console.error(err);
          this.cargando = false;
        }
      });
    }
  }
}