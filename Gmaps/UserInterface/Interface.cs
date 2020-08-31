using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Interface()
        {
            InitializeComponent();
            dm = new DataManager();
        }

        private void gMap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gmap.Position = new PointLatLng(3.42158, -76.5205);


            GMapOverlay markers = new GMapOverlay("markers");
            
        }
        
        private void setMarkers()
        {

            GMapOverlay markers = new GMapOverlay("markers");

       
            GMapMarker marker = new GMarkerGoogle(
            new PointLatLng(48.8617774, 2.349272),
            GMarkerGoogleType.red_dot);
            marker.ToolTipText = "hello\nout there";
            marker.Tag = "Hey";
            markers.Markers.Add(marker);
 
            gmap.Overlays.Add(markers);
            

        }

        private void setPolygons()
        {

            GMapOverlay polygons = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(48.866383, 2.323575));
            points.Add(new PointLatLng(48.863868, 2.321554));
            points.Add(new PointLatLng(48.861017, 2.330030));
            points.Add(new PointLatLng(48.863727, 2.331918));
            GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
            polygon.Tag = "Hi";

            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);

            polygons.Polygons.Add(polygon);
            gmap.Overlays.Add(polygons);

        }

        private void setRoutes()
        {

            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(48.866383, 2.323575));
            points.Add(new PointLatLng(48.863868, 2.321554));
            points.Add(new PointLatLng(48.861017, 2.330030));
            GMapRoute route = new GMapRoute(points, "A walk in the park");
            route.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(route);
            gmap.Overlays.Add(routes);

        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
        }

        private void gmap_OnPolygonClick(GMapPolygon item, MouseEventArgs e)
        {
            Console.WriteLine(String.Format("Polygon {0} with tag {1} was clicked",
             item.Name, item.Tag));
        }

        private void Example_Click(object sender, EventArgs e)
        {
            setMarkers();
            setPolygons();
            setRoutes();
        }

        private void randomPoint()
        {

            double lat = RandomPos.rLatitude();
            double lon = RandomPos.rLongitude();

            GMapOverlay markers = new GMapOverlay("markers");

            GMapMarker marker = new GMarkerGoogle(
            new PointLatLng(lat, lon),
            GMarkerGoogleType.red_dot);
            
            markers.Markers.Add(marker);

            gmap.Overlays.Add(markers);

        }

        private void random_Click(object sender, EventArgs e)
        {
            randomPoint();
        }

        private void municipios_Click(object sender, EventArgs e)
        {
            List<string> lista = dm.getLista();

            GMapOverlay markers = new GMapOverlay("markers");

            foreach (string f in lista)
            {

                GeoCoderStatusCode statusCode;
                PointLatLng? pointLatLng1 = OpenStreet4UMapProvider.Instance.GetPoint(f, out statusCode);

                if (pointLatLng1 != null)
                {
                    GMapMarker marker00 = new GMarkerGoogle(new PointLatLng(pointLatLng1.Value.Lat, pointLatLng1.Value.Lng), GMarkerGoogleType.blue_dot); 
                    markers.Markers.Add(marker00);

                }

            }

            gmap.Overlays.Add(markers);

        }
    }
}
