import { Routes } from '@angular/router';
import { ListadoReservasComponent } from './pages/listado-reservas/listado-reservas.component';
import { DetalleReservaComponent } from './pages/detalle-reserva/detalle-reserva.component';

export const routes: Routes = [
  // Ruta por defecto (Home) -> Listado
  { path: '', component: ListadoReservasComponent },
  
  // Ruta Detalle
  { path: 'detalle/:id', component: DetalleReservaComponent }
];