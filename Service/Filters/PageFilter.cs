// Copyright 2022 - 2023 Godwin peter .O (me@godwin.dev)
// 
// Licensed under the MIT License;
// you may not use this file except in compliance with the License.
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Reflection;
using ParkingSpace.Common.Interfaces;

namespace ParkingSpace.Filters;

public class PageFilter : IPageFilter {
    public PageFilter(int page = 1, int pageSize = 25, string search = "") {
        Page = page;
        PageSize = pageSize;
        Search = search;
    }

    public string Search { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public static ValueTask<PageFilter?> BindAsync(HttpContext httpContext, ParameterInfo parameter) {
        int.TryParse(httpContext.Request.Query["page"], out var page);
        int.TryParse(httpContext.Request.Query["page-size"], out var pageSize);

        return ValueTask.FromResult<PageFilter?>(
            new PageFilter(
                page == 0 ? 1 : page,
                pageSize == 0 ? 10 : pageSize,
                httpContext.Request.Query["search"]
            )
        );
    }
}