CREATE TABLE "Configuration" (
    "id_configuration"  INT GENERATED BY DEFAULT AS IDENTITY,
    "date"              TIMESTAMP,
    "parameter_name"    VARCHAR(255),
    "parameter_value"   VARCHAR(255),

    CONSTRAINT PK__Configuration PRIMARY KEY ("id_configuration"),

    CONSTRAINT NN__Configuration__date CHECK ("date" IS NOT NULL),
    CONSTRAINT NN__Configuration__parameter_name CHECK ("parameter_name" IS NOT NULL),
    CONSTRAINT NN__Configuration__parameter_value CHECK ("parameter_value" IS NOT NULL)
);

CREATE TABLE "Role" (
    "id_role"   INT GENERATED BY DEFAULT AS IDENTITY,
    "name"      VARCHAR(255),

    CONSTRAINT PK__Role PRIMARY KEY ("id_role"),

    CONSTRAINT U__Role__name UNIQUE ("name"),

    CONSTRAINT NN__Role__name CHECK ("name" IS NOT NULL)
);

CREATE TABLE "Role_Permission" (
    "role_id"           INT,
    "permission_id"     VARCHAR(255),

    CONSTRAINT PK_Role_Permission PRIMARY KEY ("role_id", "permission_id"),

    CONSTRAINT FK__Role_Permission__role_id FOREIGN KEY ("role_id") REFERENCES "Role"("id_role")
);

CREATE TABLE "User" (
    "id_user"                       INT GENERATED BY DEFAULT AS IDENTITY,
    "isActive"                      BOOLEAN DEFAULT true,
    "nom"                           VARCHAR(255),
    "prenom"                        VARCHAR(255),
    "email"                         VARCHAR(255),
    "password"                      VARCHAR(255),

    CONSTRAINT PK__User PRIMARY KEY ("id_user"),

    CONSTRAINT U__User__email UNIQUE ("email"),

    CONSTRAINT NN__User__isActive CHECK ("isActive" IS NOT NULL),
    CONSTRAINT NN__User__nom CHECK ("nom" IS NOT NULL),
    CONSTRAINT NN__User__prenom CHECK ("prenom" IS NOT NULL),
    CONSTRAINT NN__User__email CHECK ("email" IS NOT NULL)
);

CREATE TABLE "User_Role" (
    "user_id"   INT,
    "role_id"   INT,

    CONSTRAINT PK__User_Role PRIMARY KEY ("user_id", "role_id"),

    CONSTRAINT FK__User_Role__user_id FOREIGN KEY ("user_id") REFERENCES "User"("id_user"),
    CONSTRAINT FK__User_Role__role_id FOREIGN KEY ("role_id") REFERENCES "Role"("id_role")
);

CREATE TABLE "UserSalaire" (
    "id_userSalaire"    INT GENERATED BY DEFAULT AS IDENTITY,
    "user_id"           INT,
    "date"              TIMESTAMP,
    "regime"            INT,
    "montant"           DECIMAL,

    CONSTRAINT PK__UserSalaire PRIMARY KEY ("id_userSalaire"),

    CONSTRAINT NN__UserSalaire__user_id CHECK ("user_id" IS NOT NULL),
    CONSTRAINT NN__UserSalaire__date CHECK ("date" IS NOT NULL),
    CONSTRAINT NN__UserSalaire__regime CHECK ("regime" IS NOT NULL),
    CONSTRAINT NN__UserSalaire__montant CHECK ("montant" IS NOT NULL),

    CONSTRAINT FK__UserSalaire__user_id FOREIGN KEY ("user_id") REFERENCES "User"("id_user")
);

CREATE TABLE "WorkTime_Category" (
    "id_workTime_category"  INT GENERATED BY DEFAULT AS IDENTITY,
    "isActive"              BOOLEAN DEFAULT TRUE,
    "abreviation"           VARCHAR(5),
    "name"                  VARCHAR(255),
    "color"                 VARCHAR(8),

    CONSTRAINT PK__WorkTime_Category PRIMARY KEY ("id_workTime_category"),

    CONSTRAINT NN__WorkTime_Category__isActive CHECK ("isActive" IS NOT NULL),
    CONSTRAINT NN__WorkTime_Category__abreviation CHECK ("abreviation" IS NOT NULL),
    CONSTRAINT NN__WorkTime_Category__name CHECK ("name" IS NOT NULL),
    CONSTRAINT NN__WorkTime_Category__color CHECK ("color" IS NOT NULL)
);

CREATE TABLE "Compteur_WorkTime_Category" (
    "user_id"               INT,
    "workTime_category_id"  INT,
    "quantity"              INT DEFAULT 0,

    CONSTRAINT PK__Compteur_WorkTime_Category PRIMARY KEY ("user_id", "workTime_category_id"),

    CONSTRAINT NN__WorkTime_Category__quantity CHECK ("quantity" IS NOT NULL),
    CONSTRAINT CK__WorkTime_Category__quantity CHECK ("quantity" >= 0),

    CONSTRAINT FK__WorkTime_Category__user_id FOREIGN KEY ("user_id") REFERENCES "User"("id_user"),
    CONSTRAINT FK__WorkTime_Category__workTime_category_id FOREIGN KEY ("workTime_category_id") REFERENCES "WorkTime_Category"("id_workTime_category")
);

CREATE TABLE "Project" (
    "id_project"                INT GENERATED BY DEFAULT AS IDENTITY,
    "organisme_id"              INT,
    "isActive"                  BOOLEAN DEFAULT TRUE,
    "name"                      VARCHAR(255),
    "montant_budget"            DECIMAL,
    "color"                     VARCHAR(8),
    "date_debut_projet"         TIMESTAMP,
    "date_fin_projet"           TIMESTAMP,
    "charge_de_projet"          INT,

    CONSTRAINT PK__Project PRIMARY KEY ("id_project"),

    CONSTRAINT NN__Project__organisme_id CHECK ("organisme_id" IS NOT NULL),
    CONSTRAINT NN__Project__isActive CHECK ("isActive" IS NOT NULL),
    CONSTRAINT NN__Project__name CHECK ("name" IS NOT NULL),
    CONSTRAINT NN__Project__color CHECK ("color" IS NOT NULL),
    CONSTRAINT NN__Project__date_debut_projet CHECK ("date_debut_projet" IS NOT NULL),
    CONSTRAINT NN__Project__date_fin_projet CHECK ("date_fin_projet" IS NOT NULL),
    CONSTRAINT NN__Project__charge_de_projet CHECK ("charge_de_projet" IS NOT NULL),

    CONSTRAINT FK__Project__charge_de_projet FOREIGN KEY ("charge_de_projet") REFERENCES "User"("id_user")
);

CREATE TABLE "WorkTime" (
    "id_WorkTime"       INT GENERATED BY DEFAULT AS IDENTITY,
    "start"             TIMESTAMP,
    "end"               TIMESTAMP,
    "isOnSite"          BOOLEAN DEFAULT TRUE,
    "category_id"       INT,
    "project_id"        INT,
    "user_id"           INT,

    CONSTRAINT PK__WorkTime PRIMARY KEY ("id_WorkTime"),

    CONSTRAINT NN__WorkTime__start CHECK ("start" IS NOT NULL),
    CONSTRAINT NN__WorkTime__end CHECK ("end" IS NOT NULL),
    CONSTRAINT NN__WorkTime__isOnSite CHECK ("isOnSite" IS NOT NULL),
    CONSTRAINT NN__WorkTime__category_id CHECK ("category_id" IS NOT NULL),
    CONSTRAINT NN__WorkTime__user_id CHECK ("user_id" IS NOT NULL),

    CONSTRAINT FK__WorkTime__category_id FOREIGN KEY ("category_id") REFERENCES "WorkTime_Category"("id_workTime_category"),
    CONSTRAINT FK__WorkTime__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project"),
    CONSTRAINT FK__WorkTime__user_id FOREIGN KEY ("user_id") REFERENCES "User"("id_user")
);

CREATE TABLE "Organisme" (
    "id_organisme"      INT GENERATED BY DEFAULT AS IDENTITY,
    "name"              VARCHAR(255),

    CONSTRAINT PK__Organisme__id_organisme PRIMARY KEY ("id_organisme"),

    CONSTRAINT NN__Organisme__name CHECK ("name" IS NOT NULL),
    CONSTRAINT U__Organisme__name UNIQUE ("name")
);

CREATE TABLE "Category" (
    "id_category"               INT GENERATED BY DEFAULT AS IDENTITY,
    "name"                      VARCHAR(255),
    "isIncome"                  BOOLEAN DEFAULT FALSE,
    "EstimationParCategorie"    BOOLEAN DEFAULT TRUE,

    CONSTRAINT PK__Category PRIMARY KEY ("id_category"),

    CONSTRAINT NN__libele__name CHECK ("name" IS NOT NULL),
    CONSTRAINT NN__libele__isIncome CHECK ("isIncome" IS NOT NULL),
    CONSTRAINT NN__libele__EstimationParCategorie CHECK ("EstimationParCategorie" IS NOT NULL)
);

CREATE TABLE "Libele" (
    "id_libele"     INT GENERATED BY DEFAULT AS IDENTITY,
    "category_id"   INT,
    "name"          VARCHAR(255),

    CONSTRAINT PK__Libele PRIMARY KEY ("id_libele"),

    CONSTRAINT NN__libele__name CHECK ("name" IS NOT NULL),
    CONSTRAINT NN__libele__category_id CHECK ("category_id" IS NOT NULL),

    CONSTRAINT FK__Libele__category_id FOREIGN KEY ("category_id") REFERENCES "Category"("id_category")
);



CREATE TABLE "Prevision_Budget_Category" (
    "id_prevision_budget_category"  INT GENERATED BY DEFAULT AS IDENTITY,
    "project_id"                    INT,
    "category_id"                   INT,
    "date"                          TIMESTAMP,
    "motif"                         VARCHAR(255),
    "montant"                       DECIMAL,

    CONSTRAINT PK__Prevision_Budget_Category PRIMARY KEY ("id_prevision_budget_category"),
    CONSTRAINT U__Prevision_Budget_Category UNIQUE ("project_id", "category_id", "date"),

    CONSTRAINT NN__libele__motif CHECK ("motif" IS NOT NULL),
    CONSTRAINT NN__libele__montant CHECK ("montant" IS NOT NULL),

    CONSTRAINT FK__Prevision_Budget_Category__category_id FOREIGN KEY ("category_id") REFERENCES "Category"("id_category"),
    CONSTRAINT FK__Prevision_Budget_Category__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project")
);

CREATE TABLE "Prevision_Budget_Libele" (
    "id_prevision_budget_libele"    INT GENERATED BY DEFAULT AS IDENTITY,
    "project_id"                    INT,
    "libele_id"                     INT,
    "date"                          TIMESTAMP,
    "motif"                         VARCHAR(255),
    "montant"                       DECIMAL,


    CONSTRAINT PK__Prevision_Budget_Libele PRIMARY KEY ("id_prevision_budget_libele"),
    CONSTRAINT U__Prevision_Budget_Libele UNIQUE ("project_id", "libele_id", "date"),

    CONSTRAINT NN__libele__montant CHECK ("montant" IS NOT NULL),

    CONSTRAINT FK__Prevision_Budget_Libele__libele_id FOREIGN KEY ("libele_id") REFERENCES "Libele"("id_libele"),
    CONSTRAINT FK__Prevision_Budget_Libele__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project")
);

CREATE TABLE "Depense" (
    "id_depense"            INT GENERATED BY DEFAULT AS IDENTITY,
    "libele_id"             INT,
    "project_id"            INT,
    "organisme_id"          INT,
    "montant"               DECIMAL,
    "date_intervention"     TIMESTAMP,
    "date_facturation"      TIMESTAMP,
    "motif"                 VARCHAR(255),

    CONSTRAINT PK__Depense PRIMARY KEY ("id_depense"),

    CONSTRAINT NN__Depense__libele_id CHECK ("libele_id" IS NOT NULL),
    CONSTRAINT NN__Depense__project_id CHECK ("project_id" IS NOT NULL),
    CONSTRAINT NN__Depense__montant CHECK ("montant" IS NOT NULL),
    CONSTRAINT NN__Depense__date_intervention CHECK ("date_intervention" IS NOT NULL),

    CONSTRAINT FK__Depense__libele_id FOREIGN KEY ("libele_id") REFERENCES "Libele"("id_libele"),
    CONSTRAINT FK__Depense__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project"),
    CONSTRAINT FK__Depense__organisme_id FOREIGN KEY ("organisme_id") REFERENCES "Organisme"("id_organisme")
);

CREATE TABLE "Rentree" (
    "id_rentree"            INT GENERATED BY DEFAULT AS IDENTITY,
    "libele_id"             INT,
    "project_id"            INT,
    "organisme_id"          INT,
    "montant"               DECIMAL,
    "date_facturation"      TIMESTAMP,
    "motif"                 VARCHAR(255),

    CONSTRAINT PK__Rentree PRIMARY KEY ("id_rentree"),

    CONSTRAINT NN__Rentree__libele_id CHECK ("libele_id" IS NOT NULL),
    CONSTRAINT NN__Rentree__project_id CHECK ("project_id" IS NOT NULL),
    CONSTRAINT NN__Rentree__organisme_id CHECK ("organisme_id" IS NOT NULL),
    CONSTRAINT NN__Rentree__montant CHECK ("montant" IS NOT NULL),
    CONSTRAINT NN__Rentree__date_facturation CHECK ("date_facturation" IS NOT NULL),
    CONSTRAINT NN__Rentree__motif CHECK ("motif" IS NOT NULL),

    CONSTRAINT FK__Rentree__libele_id FOREIGN KEY ("libele_id") REFERENCES "Libele"("id_libele"),
    CONSTRAINT FK__Rentree__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project"),
    CONSTRAINT FK__Rentree__organisme_id FOREIGN KEY ("organisme_id") REFERENCES "Organisme"("id_organisme")
);

CREATE TABLE "Prevision_Rentree" (
    "id_prevision_rentree"  INT GENERATED BY DEFAULT AS IDENTITY,
    "libele_id"             INT,
    "project_id"            INT,
    "organisme_id"          INT,
    "date"                  TIMESTAMP,
    "motif"                 VARCHAR(255),
    "montant"               DECIMAL,

    CONSTRAINT PK__Prevision_Rentree PRIMARY KEY ("id_prevision_rentree"),

    CONSTRAINT NN__Prevision_Rentree__libele_id CHECK ("libele_id" IS NOT NULL),
    CONSTRAINT NN__Prevision_Rentree__project_id CHECK ("project_id" IS NOT NULL),
    CONSTRAINT NN__Prevision_Rentree__date CHECK ("date" IS NOT NULL),
    CONSTRAINT NN__Prevision_Rentree__montant CHECK ("montant" IS NOT NULL),

    CONSTRAINT FK__Prevision_Rentree__libele_id FOREIGN KEY ("libele_id") REFERENCES "Libele"("id_libele"),
    CONSTRAINT FK__Prevision_Rentree__project_id FOREIGN KEY ("project_id") REFERENCES "Project"("id_project"),
    CONSTRAINT FK__Prevision_Rentree__organisme_id FOREIGN KEY ("organisme_id") REFERENCES "Organisme"("id_organisme")
);

CREATE OR REPLACE VIEW V_GetProjects AS
SELECT
    p."id_project" AS "IdProject",
    p."isActive" AS "IsActive",
    p."name",
    p."organisme_id" AS "OrganismeId",
    o."name" AS "OrganismeName",
    p."color" AS "Color",
    p."montant_budget" AS "MontantBudget",
    p."date_debut_projet" AS "DateDebutProjet",
    p."date_fin_projet" AS "DateFinProjet",
    p."charge_de_projet" AS "ChargeDeProjetId",
    CONCAT_WS(' ', u.prenom, u.nom) AS "ChargeDeProjetName",
    prevision_depense.montant AS "PrevisionDepenseActuelle",
    depense.montant AS "DepenseReelActuelle"
FROM "Project" p
INNER JOIN "Organisme" o ON p.organisme_id = o.id_organisme
INNER JOIN "User" u ON p.charge_de_projet = u.id_user
LEFT JOIN (
    SELECT project_id, SUM(montant) AS montant
    FROM (
        SELECT project_id, montant, date
        FROM "Prevision_Budget_Category"
        UNION ALL
        SELECT project_id,montant, date
        FROM "Prevision_Budget_Libele"
    ) prevision_depense
    WHERE date <= NOW()
    GROUP BY project_id
) prevision_depense ON prevision_depense.project_id = p.id_project
LEFT JOIN (
    SELECT project_id, SUM(montant) AS "montant"
    FROM "Depense"
    WHERE date_facturation <= NOW()
    GROUP BY project_id
) depense ON depense.project_id = p.id_project
ORDER BY "DateDebutProjet" DESC;



CREATE FUNCTION get_cumulative_rentree(id INT)
RETURNS TABLE (
    "month_year" TEXT,
    "cumulative_montant_prevision" NUMERIC,
    "cumulative_montant_reel" NUMERIC
) AS $$
BEGIN
    RETURN QUERY
        WITH inOut_prevision AS (
            SELECT c.id_category idCategory, c.name nameCategory, TO_CHAR(d.date, 'MM-YYYY') "date", SUM(d.montant) montant
            FROM "Prevision_Rentree" d
            INNER JOIN "Libele" l ON d.libele_id = l.id_libele
            INNER JOIN "Category" c ON l.category_id = c.id_category
            WHERE project_id = id
            AND c."isIncome" = true
            AND d.date <= NOW()
            GROUP BY c.name, c.id_category, "date"

            UNION

            SELECT c.id_category, c.name, TO_CHAR(d.date, 'MM-YYYY'), SUM(d.montant)
            FROM "Prevision_Budget_Category" d
            INNER JOIN "Category" c ON d.category_id = c.id_category
            WHERE project_id = id
            AND c."isIncome" = true
            AND d.date <= NOW()
            GROUP BY c.name, c.id_category, "date"
        ),
        inOut_Reel AS (
            SELECT c.id_category idCategory, c.name nameCategory, TO_CHAR(d.date_facturation, 'MM-YYYY') "date", SUM(d.montant) montant
            FROM "Rentree" d
            INNER JOIN "Libele" l ON d.libele_id = l.id_libele
            INNER JOIN "Category" c ON l.category_id = c.id_category
            WHERE project_id = id
            AND d.date_facturation <= NOW()
            GROUP BY c.name, c.id_category, "date"
        ),
        all_combinations AS (
            SELECT categories.idCategory, categories.nameCategory, calendar."date"
            FROM (
                SELECT DISTINCT TO_CHAR(GENERATE_SERIES(date_debut_projet, date_fin_projet, '1 month'), 'MM-YYYY') AS "date"
                FROM "Project"
                WHERE id_project = id
            ) calendar
            CROSS JOIN (
                SELECT DISTINCT idCategory, nameCategory
                FROM inOut_prevision
            ) categories
        ),
        monthly_totals AS (
            SELECT
            ac."date",
            COALESCE(SUM(p.montant), 0) AS montant_previ,
            COALESCE(SUM(r.montant), 0) AS montant_reel
            FROM all_combinations ac
            LEFT JOIN inOut_prevision p ON p.date = ac."date" AND p.idCategory = ac.idCategory
            LEFT JOIN inOut_Reel r ON r.date = ac."date" AND r.idCategory = ac.idCategory
            GROUP BY ac."date"
        )
        SELECT
            mt."date",
            SUM(mt.montant_previ) OVER (ORDER BY mt."date") AS cumulative_montant_prevision,
            SUM(mt.montant_reel) OVER (ORDER BY mt."date") AS cumulative_montant_reel
        FROM monthly_totals mt
        ORDER BY mt."date";
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_cumulative_depense(id INT)
RETURNS TABLE (
    "month_year" TEXT,
    cumulative_montant_prevision NUMERIC,
    cumulative_montant_reel NUMERIC
) AS $$
BEGIN
    RETURN QUERY
        WITH inOut_prevision AS (
            SELECT c.id_category idCategory, c.name nameCategory, TO_CHAR(d.date, 'MM-YYYY') "date", SUM(d.montant) montant
            FROM "Prevision_Budget_Libele" d
            INNER JOIN "Libele" l ON d.libele_id = l.id_libele
            INNER JOIN "Category" c ON l.category_id = c.id_category
            WHERE project_id = id
            AND c."isIncome" = false
            AND d.date <= NOW()
            GROUP BY c.name, c.id_category, "date"

            UNION

            SELECT c.id_category, c.name, TO_CHAR(d.date, 'MM-YYYY'), SUM(d.montant)
            FROM "Prevision_Budget_Category" d
            INNER JOIN "Category" c ON d.category_id = c.id_category
            WHERE project_id = id
            AND c."isIncome" = false
            AND d.date <= NOW()
            GROUP BY c.name, c.id_category, "date"
        ),
        inOut_Reel AS (
            SELECT c.id_category idCategory, c.name nameCategory, TO_CHAR(d.date_facturation, 'MM-YYYY') "date", SUM(d.montant) montant
            FROM "Depense" d
            INNER JOIN "Libele" l ON d.libele_id = l.id_libele
            INNER JOIN "Category" c ON l.category_id = c.id_category
            WHERE project_id = id
            AND d.date_facturation <= NOW()
            GROUP BY c.name, c.id_category, "date"
        ),
        all_combinations AS (
            SELECT categories.idCategory, categories.nameCategory, calendar."date"
            FROM (
                SELECT DISTINCT TO_CHAR(GENERATE_SERIES(date_debut_projet, NOW(), '1 month'), 'MM-YYYY') AS "date"
                FROM "Project"
                WHERE id_project = id
            ) calendar
            CROSS JOIN (
                SELECT DISTINCT idCategory, nameCategory
                FROM inOut_prevision
            ) categories
        ),
        monthly_totals AS (
            SELECT
            ac."date",
            COALESCE(SUM(p.montant), 0) AS montant_previ,
            COALESCE(SUM(r.montant), 0) AS montant_reel
            FROM all_combinations ac
            LEFT JOIN inOut_prevision p ON p.date = ac."date" AND p.idCategory = ac.idCategory
            LEFT JOIN inOut_Reel r ON r.date = ac."date" AND r.idCategory = ac.idCategory
            GROUP BY ac."date"
        )
        SELECT
            mt."date",
            SUM(mt.montant_previ) OVER (ORDER BY mt."date") AS cumulative_montant_prevision,
            SUM(mt.montant_reel) OVER (ORDER BY mt."date") AS cumulative_montant_reel
        FROM monthly_totals mt
        ORDER BY mt."date";
END;
$$ LANGUAGE plpgsql;
