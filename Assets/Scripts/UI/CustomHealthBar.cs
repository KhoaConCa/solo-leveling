using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platform2D.UIElement
{
    /// <summary>
    /// CustomHealthBar - Thiết lập và điều chỉnh Health bar.
    /// Tác giả: Nguyễn Ngọc Phú, Ngày tạo: 24/05/2025.
    /// </summary>
    public class CustomHealthBar : MonoBehaviour
    {
        #region --- Methods ---

        /// <summary>
        /// Đặt giá trị mặc định cho Health bar.
        /// </summary>
        /// <param name="maxHealth"> Máu tối đa của vật thể. </param>
        /// <param name="isTarget"> Vật thế là một Player. </param>
        public void SetMaxHealth(float maxHealth, bool isTarget = false)
        {
            _healthBar.maxValue = maxHealth;
            _healthBar.value = maxHealth;

            if (isTarget)
                _healthFill.color = _healthBarGradient.Evaluate(1f);

            if (_isShowHp && isTarget)
            {
                Text curHp = this.gameObject.GetComponentInChildren<Text>();
                _maxHp = maxHealth;
                curHp.text = $"{maxHealth} / {_maxHp}";
            }
        }

        /// <summary>
        /// Thay đổi giá trị của Health bar.
        /// </summary>
        /// <param name="curHealth"> Giá trị hiện tại của vật thể. </param>
        /// <param name="isTarget"> Vật thế là một Player. </param>
        public void ChangeHealth(float curHealth, bool isTarget = false)
        {
            _healthBar.value = curHealth;
            if (isTarget)
                _healthFill.color = _healthBarGradient.Evaluate(_healthBar.normalizedValue);

            if (_isShowHp && isTarget)
            {
                Text curHp = this.gameObject.GetComponentInChildren<Text>();
                curHp.text = $"{curHealth} / {_maxHp}";
            }
        }

        public void ChangeText(string text)
        {
            Text name = this.gameObject.GetComponentInChildren<Text>();
            name.text = text;
        }

        #endregion

        #region --- Fields ---

        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _healthFill;

        [SerializeField] private Gradient _healthBarGradient;

        [SerializeField] private bool _isShowHp = false;
        [SerializeField] private float _maxHp;

        #endregion
    }
}
