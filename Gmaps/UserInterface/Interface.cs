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
        private List<PointLatLng> puntos;
        private List<PointLatLng> poligonos;
        private List<PointLatLng> rutas;

        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay polygons = new GMapOverlay("polygons");
        GMapOverlay routes = new GMapOverlay("routes");

        public Interface()
        {
            InitializeComponent();
            dm = new DataManager();
            
            puntos = new List<PointLatLng>();
            poligonos = new List<PointLatLng>();
            rutas = new List<PointLatLng>();
        }

        private void gMap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gmap.Position = new PointLatLng(3.42158, -76.5205);
            
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(routes);

        }
        
        private void setMarkers()
        {
            foreach(PointLatLng p in puntos)
            {
                GMapMarker marker = new GMarkerGoogle(p, GMarkerGoogleType.red_dot);
                markers.Markers.Add(marker);
            }

        }

        private void setPolygons()
        {
            GMapPolygon polygon = new GMapPolygon(poligonos, "Polygon");

            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);

            polygons.Polygons.Add(polygon);
        }

        private void setRoutes()
        {
            GMapRoute route = new GMapRoute(rutas, "Route");
            route.Stroke = new Pen(Color.Red, 3);
            Console.WriteLine(route.Distance);
            routes.Routes.Add(route);
        }

        private void municipios_Click(object sender, EventArgs e)
        {
            List<string> lista = dm.getLista();

            foreach (string f in lista)
            {
                GeoCoderStatusCode statusCode;
                PointLatLng? pointLatLng1 = OpenStreet4UMapProvider.Instance.GetPoint(f, out statusCode);

                if (pointLatLng1 != null)
                {
                    GMapMarker marker00 = new GMarkerGoogle(new PointLatLng(pointLatLng1.Value.Lat, pointLatLng1.Value.Lng), GMarkerGoogleType.blue_dot);
                    marker00.ToolTipText = f + "\n" + pointLatLng1.Value.Lat + "\n" + pointLatLng1.Value.Lng;
                    markers.Markers.Add(marker00);
                    
                }

            }
        }


        private void deletePoints_click(object sender, EventArgs e)
        {
            puntos.Clear();
            markers.Clear();
            polygons.Clear();
            routes.Clear();
        }

        private void Add_Click(object sender, EventArgs e)
        {

            double lat = double.Parse(lat_textBox.Text);
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

        private void Show_click(object sender, EventArgs e)
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
