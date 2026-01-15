import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router'; // Importante para el botón
import { Reserva, ReservaService } from '../../services/reserva.service';

@Component({
  selector: 'app-listado-reservas',
  standalone: true,
  imports: [CommonModule, DatePipe, RouterLink], // Importamos las herramientas
  templateUrl: './listado-reservas.component.html',
  styleUrl: './listado-reservas.component.css'
})
export class ListadoReservasComponent implements OnInit {
  
  reservas: Reserva[] = [];
  reservaService = inject(ReservaService);

  ngOnInit(): void {
    this.cargarDatos();
  }

  cargarDatos() {
    this.reservaService.getReservas().subscribe({
      next: (data) => this.reservas = data.items,
      error: (err) => console.error(err)
    });
  }
}