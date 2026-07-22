## ApiResult
- Success : bool
- Message : string
- Data : T

## BloqueDisponibilidad
- Id : int
- IdEquipo : int
- DiaSemana : string
- HoraInicio : TimeSpan
- HoraFin : TimeSpan
- TipoReserva : string

## CategoriaDocumento
- Id : int
- Nombre : string
- Descripcion : string?

## Documento
- Id : int
- Titulo : string
- Descripcion : string
- CategoriaId : int
- UrlArchivo : string
- FechaSubida : DateTime
- IdUsuario : int
- IdLoteCultivo : int?
- IdProyecto : int?

## EntradaDiario
- Id : int
- IdProyecto : int
- IdUsuario : int
- IdLoteCultivo : int?
- FechaRegistro : DateTime
- Titulo : string
- Contenido : string

## Equipo
- Id : int
- CodigoInventario : string
- Nombre : string
- Marca : string
- Modelo : string
- Estado : string
- FechaProximoMantenimiento : DateTime?

## Especie
- Id : int
- IdTaxonomia : int
- IdTipoPlanta : int
- CodigoEstricto : string
- NombreCientifico : string
- NombreComun : string
- SinonimosTaxonomicos : string?
- CicloVida : string
- CategoriaUso : string
- EstadoConservacion : string
- EsEndemica : bool
- EsNativa : bool
- DistribucionNatural : string
- ImportanciaEspecie : string
- DiasResiembra : short
- MaxResiembras : short
- Activo : bool

## Familia
- Id : int
- NombreFamilia : string

## FaseCultivo
- Id : int
- NombreFase : string

## Genero
- Id : int
- NombreGenero : string

## KardexMovimiento
- Id : int
- IdReactivo : int
- IdUsuario : int
- TipoMovimiento : string
- Cantidad : decimal
- Motivo : string?
- FechaMovimiento : DateTime

## LoteCultivo
- Id : int
- IdPlantaMadre : int
- IdLotePadre : int?
- IdUnidadFrascoOrigen : int?
- IdMedioCultivo : int
- IdProyecto : int
- IdUbicacionFisica : int
- IdFaseCultivo : int
- IdUsuario : int
- CodigoTrazabilidad : string
- NumeroRepique : int
- FechaSiembra : DateTime
- TotalUnidades : int
- TotalExplantesIntroducidos : int
- ChecklistSiembra : string?
- UrlQr : string?
- TipoExplante : string
- ExplantesPorUnidad : int
- IdDocumentoProtocolo : int?
- Activo : bool

## MedioCultivo
- Id : int
- Siglas : string
- Descripcion : string
- Componentes : string
- IdUsuarioPropietario : int?

## MonitoreoFitosanitario
- Id : int
- IdUnidadFrasco : int
- IdUsuario : int
- FechaEvaluacion : DateTime
- UnidadesRevisadas : int
- NivelFenolizacion : string
- NivelContaminacion : string
- Bacterias : bool
- Hongos : bool
- Muerte : bool
- Respuesta : string
- RequiereResiembra : bool
- Observaciones : string

## PermisoAmbiental
- Id : int
- IdPlantaMadre : int
- NumeroResolucion : string
- UrlArchivoPdf : string
- FechaEmision : DateTime

## PlantaMadre
- Id : int
- IdEspecie : int
- IdProyecto : int
- IdUsuario : int
- IdResponsableIntroduccion : int?
- CodigoAsignado : string
- FechaRecepcion : DateTime
- EstadoFitosanitario : string
- UrlFotografia : string?
- UrlQr : string?
- Procedencia : string
- FechaColecta : DateTime?
- IdResponsableColecta : int?
- ResponsablesColectaNombres : string?
- IdDocumentoPermiso : int?
- IdProtocolo : int?
- HerbarioReferencia : string?
- Observaciones : string?
- Activo : bool

## Protocolo
- Id : int
- IdEspecie : int?
- IdUsuarioAutor : int
- FaseProtocolo : string
- Titulo : string
- Descripcion : string
- UrlArchivoPdf : string?
- FechaCreacion : DateTime
- Activo : bool

## Proyecto
- Id : int
- NombreProyecto : string
- Descripcion : string
- TipoProyecto : string
- Estado : string
- FechaInicio : DateTime
- FechaFin : DateTime?
- IdUsuarioResponsable : int
- IdEspecie : int?
- IdDirector : int?
- IdTesista : int?
- DirectoresNombres : string?
- EstudiantesNombres : string?
- Uso : string?
- PlantasMadresConfirmadas : bool

## ProyectoUsuario
- Id : int
- IdProyecto : int
- IdUsuario : int
- RolEnProyecto : string

## Reactivo
- Id : int
- Nombre : string
- StockActual : decimal
- StockMinimo : decimal
- IdUnidadMedida : int
- Categoria : string
- FechaCaducidad : DateTime?
- Marca : string?
- ProveedorNombre : string?
- ProveedorTelefono : string?
- ProveedorSucursal : string?
- ProveedorEmail : string?
- ProveedorContacto : string?
- ProveedorRUC : string?

## ReservaEquipo
- Id : int
- IdEquipo : int
- IdUsuario : int
- IdBloque : int?
- FechaReserva : DateTime
- HoraInicio : TimeSpan
- HoraFin : TimeSpan
- Estado : string
- ObservacionesCheckOut : string?
- IdSolicitud : int?

## Rol
- Id : int
- NombreRol : string

## SolicitudDetalleMaterial
- Id : int
- IdSolicitud : int
- IdReactivo : int
- CantidadSolicitada : decimal
- Observacion : string?

## SolicitudLab
- Id : int
- IdProyecto : int
- IdSolicitante : int
- IdAprobador : int?
- Estado : string
- TipoSolicitud : string
- Prioridad : string
- PrioridadReactivos : string?
- FechaReactivos : DateTime?
- HoraReactivos : string?
- PrioridadImplementos : string?
- FechaImplementos : DateTime?
- HoraImplementos : string?
- Observaciones : string?
- MotivoRechazo : string?
- FechaSolicitud : DateTime
- FechaGestion : DateTime?
- IdEquipo : int?
- FechaReservaEquipo : DateTime?
- HoraInicioEquipo : string?
- HoraFinEquipo : string?

## TareaOperativa
- Id : int
- IdUsuario : int
- TipoTarea : string
- Estado : string
- FechaAsignacion : DateTime
- FechaCompletada : DateTime?

## Taxonomia
- Id : int
- Dominio : string
- Reino : string
- SubReino : string?
- FiloDivision : string
- Clase : string
- Orden : string
- Familia : string
- SubFamilia : string?
- Tribu : string?
- SubTribu : string?
- Genero : string
- Especie : string

## TipoCultivo
- Id : int
- NombreTipo : string

## TipoIdentificacion
- Id : int
- NombreTipo : string

## TipoPlanta
- Id : int
- Nombre : string

## UbicacionFisica
- Id : int
- CodigoAnaquel : string
- NombreCuerpo : string
- NumeroPiso : int
- LimiteFrascos : int
- EstadoUbicacion : string

## UnidadFrasco
- Id : int
- IdLoteCultivo : int
- CodigoUnidad : string
- NumeroResiembra : int
- Estado : string
- UrlQr : string?
- Activo : bool

## UnidadMedida
- Id : int
- Nombre : string
- Abreviatura : string
- UnidadBase : string
- FactorConversion : decimal

## Usuario
- Id : int
- IdRol : int
- IdTipoIdentificacion : int
- NumeroIdentificacion : string
- IdGenero : int
- Nombres : string
- Apellidos : string
- Email : string
- Telefono : string?
- ContrasenaHash : string
- IntentosFallidos : int
- CuentaBloqueada : bool
- Activo : bool


