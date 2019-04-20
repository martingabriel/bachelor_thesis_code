using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StudentsNotifier.Models
{
    public partial class RozvrhoveAkce
    {
        [JsonProperty("rozvrhovaAkce")]
        public List<RozvrhovaAkce> RozvrhovaAkce { get; set; }
    }

    public partial class RozvrhovaAkce
    {
        [JsonProperty("roakIdno")]
        public long? RoakIdno { get; set; }

        [JsonProperty("nazev")]
        public string Nazev { get; set; }

        [JsonProperty("katedra")]
        public string Katedra { get; set; }

        [JsonProperty("predmet")]
        public string Predmet { get; set; }

        [JsonProperty("statut")]
        public string Statut { get; set; }

        [JsonProperty("ucitIdno")]
        public long? UcitIdno { get; set; }

        [JsonProperty("ucitel")]
        public Ucitel Ucitel { get; set; }

        [JsonProperty("rok")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Rok { get; set; }

        [JsonProperty("budova")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Budova { get; set; }

        [JsonProperty("mistnost")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Mistnost { get; set; }

        [JsonProperty("kapacitaMistnosti")]
        public long? KapacitaMistnosti { get; set; }

        [JsonProperty("planObsazeni")]
        public long PlanObsazeni { get; set; }

        [JsonProperty("obsazeni")]
        public long Obsazeni { get; set; }

        [JsonProperty("typAkce")]
        public string TypAkce { get; set; }

        [JsonProperty("typAkceZkr")]
        public string TypAkceZkr { get; set; }

        [JsonProperty("semestr")]
        public string Semestr { get; set; }

        [JsonProperty("platnost")]
        public string Platnost { get; set; }

        [JsonProperty("den")]
        public string Den { get; set; }

        [JsonProperty("denZkr")]
        public string DenZkr { get; set; }

        [JsonProperty("vyucJazyk")]
        public object VyucJazyk { get; set; }

        [JsonProperty("hodinaOd")]
        public object HodinaOd { get; set; }

        [JsonProperty("hodinaDo")]
        public object HodinaDo { get; set; }

        [JsonProperty("hodinaSkutOd")]
        public HodinaSkutDo HodinaSkutOd { get; set; }

        [JsonProperty("hodinaSkutDo")]
        public HodinaSkutDo HodinaSkutDo { get; set; }

        [JsonProperty("tydenOd")]
        public long TydenOd { get; set; }

        [JsonProperty("tydenDo")]
        public long TydenDo { get; set; }

        [JsonProperty("tyden")]
        public string Tyden { get; set; }

        [JsonProperty("tydenZkr")]
        public string TydenZkr { get; set; }

        [JsonProperty("grupIdno")]
        public object GrupIdno { get; set; }

        [JsonProperty("jeNadrazena")]
        public string JeNadrazena { get; set; }

        [JsonProperty("maNadrazenou")]
        public string MaNadrazenou { get; set; }

        [JsonProperty("kontakt")]
        public string Kontakt { get; set; }

        [JsonProperty("krouzky")]
        public object Krouzky { get; set; }

        [JsonProperty("casovaRada")]
        public object CasovaRada { get; set; }

        [JsonProperty("datum")]
        public HodinaSkutDo Datum { get; set; }

        [JsonProperty("datumOd")]
        public HodinaSkutDo DatumOd { get; set; }

        [JsonProperty("datumDo")]
        public HodinaSkutDo DatumDo { get; set; }

        [JsonProperty("druhAkce")]
        public string DruhAkce { get; set; }

        [JsonProperty("vsichniUciteleUcitIdno")]
        public string VsichniUciteleUcitIdno { get; set; }

        [JsonProperty("vsichniUciteleJmenaTituly")]
        public string VsichniUciteleJmenaTituly { get; set; }

        [JsonProperty("vsichniUciteleJmenaTitulySPodily")]
        public string VsichniUciteleJmenaTitulySPodily { get; set; }

        [JsonProperty("vsichniUcitelePrijmeni")]
        public string VsichniUcitelePrijmeni { get; set; }

        [JsonProperty("referencedIdno")]
        public long ReferencedIdno { get; set; }

        [JsonProperty("poznamkaRozvrhare")]
        public object PoznamkaRozvrhare { get; set; }

        [JsonProperty("nekonaSe")]
        public object NekonaSe { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("zakazaneAkce")]
        public object ZakazaneAkce { get; set; }
    }

    public partial class HodinaSkutDo
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Ucitel
    {
        [JsonProperty("ucitIdno")]
        public long UcitIdno { get; set; }

        [JsonProperty("jmeno")]
        public string Jmeno { get; set; }

        [JsonProperty("prijmeni")]
        public string Prijmeni { get; set; }

        [JsonProperty("titulPred")]
        public string TitulPred { get; set; }

        [JsonProperty("titulZa")]
        public string TitulZa { get; set; }

        [JsonProperty("platnost")]
        public string Platnost { get; set; }

        [JsonProperty("zamestnanec")]
        public string Zamestnanec { get; set; }

        [JsonProperty("podilNaVyuce")]
        public object PodilNaVyuce { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
