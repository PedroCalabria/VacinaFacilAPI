using System.Collections;
using System.Security.Claims;
using VacinaFacil.Utils.PatientContext;

namespace VacinaFacil.Utils.Extensions
{
    public static class PatientContextExtensions
    {
        public static string Email(this IPatientContext patientContext)
        {
            var email = patientContext.GetClaimValue<string>("email");

            return email ?? Environment.MachineName;
        }

        public static int Id(this IPatientContext patientContext)
        {
            int.TryParse(patientContext.GetClaimValue<string>("id"), out var id);

            return id;
        }

        public static void AddData<TValue>(this IPatientContext patientContext, string key, TValue data)
        {
            patientContext.AdditionalData ??= new Hashtable();

            if (!patientContext.AdditionalData.ContainsKey(key))
                patientContext.AdditionalData.Add(key, data);
            else
                patientContext.AdditionalData[key] = data;
        }

        private static TResult? GetClaimValue<TResult>(this IPatientContext patientContext, string key)
        {
            if (patientContext?.AdditionalData is Hashtable additionalData && additionalData.ContainsKey(key))
                try { return (TResult)additionalData[key]; } catch { return default; }

            return default;
        }
    }
}