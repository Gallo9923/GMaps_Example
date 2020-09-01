using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using Gmaps.Model;

namespace Gmaps
{
    public partial class Interface : Form
    {

        private DataManager dm;

        //Use tres listas para comodidad

        private List<PointLatLng> puntos;       //Lista de puntos para los Marcadores
        private List<PointLatLng> poligonos;    //Lista de puntos para los poligonos
        private List<PointLatLng> rutas;        //Lista de puntos para las rutas

        GMapOverlay markers = new GMapOverlay("markers");   //La "capa" donde iran los Marcadores
        GMapOverlay polygons = new GMapOverlay("polygons"); //La "capa" donde iran los poligonos
        GMapOverlay routes = new GMapOverlay("routes");     //La "capa" donde iran los rutas  

        public Interface()
        {
            InitializeComponent();
            dm = new DataManager();
            
            puntos = new List<PointLatLng>();
            poligonos = new List<PointLatLng>();
            rutas = new List<PointLatLng>();
        }

        private void gMap_Load(object sender, EventArgs e) //Carga el GmapControl
        {
            gmap.MapProvider = GoogleMapProvider.Instance;  //Proveedor del servicio
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gmap.Position = new PointLatLng(3.42158, -76.5205); //No necesario, simplemente da una posicion para empezar
            

            // Se agregan las "capas" al conjunto de capas del GmapControl
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(routes);

        }
        
        private void setMarkers()   //Coloca los marcadores en la capa
        {
            foreach(PointLatLng p in puntos) //P es un punto creado con latitud y longitud
            {
                GMapMarker marker = new GMarkerGoogle(p, GMarkerGoogleType.red_dot);
                markers.Markers.Add(marker); //Aqui se agrega el marcador a la capa
            }

        }

        private void setPolygons() //Extrapolar la explicación de los marcadores aqui...
        {
            GMapPolygon polygon = new GMapPolygon(poligonos, "Polygon");

            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red)); //Lineas para modificar apariencia
            polygon.Stroke = new Pen(Color.Red, 1);                       //No necesarias  

            polygons.Polygons.Add(polygon);
        }

        private void setRoutes() //Extrapolar la explicación de los marcadores aqui...
        {
            GMapRoute route = new GMapRoute(rutas, "Route");
            route.Stroke = new Pen(Color.Red, 3);
            Console.WriteLine(route.Distance); //Metodo que retorna la distancia comprendida en la ruta (KM)
            routes.Routes.Add(route);
        }

        private void municipios_Click(object sender, EventArgs e)   //Mostrar municipios de Colombia
        {
            List<string> lista = dm.getLista(); //Trae los nombres de los municipios desde el Model

            foreach (string f in lista)
            {
                GeoCoderStatusCode statusCode;  
                PointLatLng? pointLatLng1 = OpenStreet4UMapProvider.Instance.GetPoint(f, out statusCode);

                //Las anteriores dos lineas proveen las funcionalidades para hacer la georeferenciación inversa

                if (pointLatLng1 != null)
                {
                    GMapMarker marker00 = new GMarkerGoogle(new PointLatLng(pointLatLng1.Value.Lat, pointLatLng1.Value.Lng), GMarkerGoogleType.blue_dot);
                    marker00.ToolTipText = f + "\n" + pointLatLng1.Value.Lat + "\n" + pointLatLng1.Value.Lng; // Esta linea es solo apariencia
                    markers.Markers.Add(marker00);
                    
                }

            }
        }


        private void deletePoints_click(object sender, EventArgs e) //Limpia todas las capas
        {
            puntos.Clear();
            markers.Clear();
            polygons.Clear();
            routes.Clear();
        }

        private void Add_Click(object sender, EventArgs e) //Añade PointsLatLng a la lista especificada en el comboBox
        {

            double lat = double.Parse(lat_textBox.Text); //Si te pc esta en español, usa comas con los decimales
            lat_textBox.Text = "";
            double lon = double.Parse(lon_textBox.Text);
            lon_textBox.Text = "";

            PointLatLng p = new PointLatLng(lat, lon);

            if (comboBox.Text == "Marker")
                puntos.Add(p);
            else if (comboBox.Text == "Polygon")
                poligonos.Add(p);
            else
                rutas.Add(p);
        }

        private void Show_click(object sender, EventArgs e) //Mostrar contenido de capas
        {
            if (comboBox.Text == "Marker")
            {
                setMarkers();
                puntos.Clear();
            }
            else if (comboBox.Text == "Polygon")
            { 
                setPolygons();
                poligonos.Clear();
            }

            else 
            {
                setRoutes();
                rutas.Clear();
            }
               
        }
    }
}
