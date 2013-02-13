
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using CombatManager;
using Android.Webkit;

namespace CombatManagerDroid
{
    public class CombatFragment : Fragment
    {
        static CombatState _CombatState;
        static Character _ViewCharacter;

        public override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            if (_CombatState == null)
            {
                _CombatState = new CombatState();

                for (int i=0; i<12; i++)
                {
                    Character c = new Character(Monster.Monsters[i], true);
                    c.IsMonster = (i%2==0)?true:false;
                    _CombatState.AddCharacter(c);
                }
            }
            _CombatState.PropertyChanged += HandleCombatStatePropertyChanged;


        }

        void HandleCombatStatePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCharacter")
            {
                UpdateCurrentCharacter(View);
                ((BaseAdapter)View.FindViewById<ListView>
                    (Resource.Id.initiativeList).Adapter).NotifyDataSetChanged();
            }
        }

        public override Android.Views.View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.Combat, container, false);

            v.FindViewById<Button>(Resource.Id.nextButton).Click += 
                delegate {NextClicked();};
            v.FindViewById<Button>(Resource.Id.prevButton).Click += 
                delegate {PrevClicked();};
            v.FindViewById<Button>(Resource.Id.upButton).Click += 
                delegate {UpClicked();};
            v.FindViewById<Button>(Resource.Id.downButton).Click += 
                delegate {DownClicked();};
            v.FindViewById<Button>(Resource.Id.rollInitiativeButton).Click += 
            delegate {RollInitiativeClicked();};

            
            UpdateCurrentCharacter(v);

            ListView lv = v.FindViewById<ListView>(Resource.Id.initiativeList);
            lv.SetAdapter (
                new InitiativeListAdapter(_CombatState));
            
            lv.ItemClick +=  (sender, e) => {
                Character c = ((BaseAdapter<Character>)lv.Adapter)[e.Position];
                ShowCharacter(v, c);
            };

            AddCharacterList(inflater, container, v, Resource.Id.playerListLayout, true);
            AddCharacterList(inflater, container, v, Resource.Id.monsterListLayout, false);

            ShowCharacter(v, _ViewCharacter);

            return v;
        }

        private void AddCharacterList(LayoutInflater inflater, ViewGroup container, View v, int id, bool monsters)
        {
            LinearLayout cl = (LinearLayout)inflater.Inflate(Resource.Layout.CharacterList, container, false);

            cl.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent, 1f);

            ListView lv = cl.FindViewById<ListView>(Resource.Id.characterList);

            lv.SetAdapter(new CharacterListAdapter(_CombatState, monsters));
            lv.ItemSelected += (sender, e) => {
                Character c = ((BaseAdapter<Character>)lv.Adapter)[e.Position];
                ShowCharacter(v, c);
            };
            lv.ItemClick +=  (sender, e) => {
                Character c = ((BaseAdapter<Character>)lv.Adapter)[e.Position];
                ShowCharacter(v, c);
            };
            v.FindViewById<LinearLayout>(id).AddView(cl);

        }

        private void ShowCharacter(View v, Character c)
        {
            if (c != null)
            {
                WebView wv = v.FindViewById<WebView>(Resource.Id.characterView);
                wv.LoadUrl("about:blank");
                wv.LoadData(MonsterHtmlCreator.CreateHtml(c.Monster), "text/html", null);
            }
            _ViewCharacter = c;
        }


        private void NextClicked()
        {
            _CombatState.MoveNext();
        }
        private void PrevClicked()
        {
            _CombatState.MovePrevious();
        }
        private void UpClicked()
        {
            
        }
        private void DownClicked()
        {
            
        }
        private void RollInitiativeClicked()
        {
            _CombatState.RollInitiative();
            _CombatState.SortInitiative();
        }
        private void UpdateCurrentCharacter(View v)
        {
           
            v.FindViewById<TextView>(Resource.Id.characterText).Text 
                = _CombatState.CurrentCharacter == null?"":_CombatState.CurrentCharacter.Name;

            v.FindViewById<TextView>(Resource.Id.roundText).Text =
                "Round " + (_CombatState.Round==null?"":_CombatState.Round.ToString());
        }


        public override void OnSaveInstanceState (Bundle outState)
        {
            base.OnSaveInstanceState (outState);
        }



        public override void OnDestroy ()
        {
            base.OnDestroy();

            _CombatState.PropertyChanged -= HandleCombatStatePropertyChanged;
        }
    }
}

