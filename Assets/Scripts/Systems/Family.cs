using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

public class Family
{
    [Serializable]
    public enum States
    {
        Healthy,
        Sick,
        Hungry,
        Cold,
        Dead
    }

    [Serializable]
    public enum Relationships
    {
        Dad,
        Partner,
        Son,
        Daughter
    }

    private Dictionary<Relationships, States> _familyMembers = new()
    {
        { Relationships.Dad, States.Healthy },
        { Relationships.Partner, States.Healthy },
        { Relationships.Son, States.Healthy },
        { Relationships.Daughter, States.Healthy }
    };

    public void UpdateFamilyMemberState(Relationships member, States state)
    {
        if (_familyMembers.ContainsKey(member))
        {
            _familyMembers[member] = state;
        }
        else
        {
            // Debug.LogError("Family member not found");
        }
    }

    public States GetFamilyMemberState(Relationships member)
    {
        if (_familyMembers.TryGetValue(member, out var state))
        {
            return state;
        }

        Debug.LogError("Family member not found");
        return States.Healthy; // Default state
    }

    public Dictionary<Relationships, States> GetFamilyMembers()
    {
        return _familyMembers;
    }

    public Relationships[] GetMembersAlive =>
        (from member in _familyMembers where member.Value != States.Dead select member.Key).ToArray();

    public void SaveFamily()
    {
        var data = new Dictionary<string, object>();
        foreach (var member in _familyMembers)
        {
            data[member.Key.ToString()] = member.Value.ToString();
        }

        var a = new Dictionary<string, object> { { "family", data } };

        Save.SaveData(a);
    }

    public void LoadFamily()
    {
        var data = Save.LoadDirectly();
        if (!data.TryGetValue("family", out var value))
        {
            Debug.LogError("Family data not found");
            SaveFamily();
            return;
        }

        var familyData = JsonConvert.DeserializeObject<Dictionary<string, object>>(value.ToString());
        foreach (var member in familyData.Where(member => member.Value != null).Where(member => member.Key != null))
        {
            if (Enum.TryParse(member.Key, out Relationships relationship) &&
                Enum.TryParse(member.Value.ToString(), out States state))
            {
                _familyMembers[relationship] = state;
            }
        }
    }
}